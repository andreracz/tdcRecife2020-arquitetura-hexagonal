using System;
using hexagonal_ddd.Core.Domain;
using hexagonal_ddd.Core.Ports;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using hexagonal_ddd.Adapter.Repository.Entities;

namespace hexagonal_ddd.Adapter.Repository
{

    public class ContaCorrenteRepository: DbContext, IContaCorrenteRepository 
    {

		public ContaCorrenteRepository(DbContextOptions<ContaCorrenteRepository> options)
			: base(options) {
		}

		private DbSet<ContaCorrenteEntity> Contas { get; set; }

		private DbSet<TransacaoEntity> Transacoes { get; set; }

		private static List<Transacao> mapearTransacoes(List<TransacaoEntity> trans) {
			List<Transacao> transacaos = new List<Transacao>();
			foreach (var tran in trans)
			{
				transacaos.Add(new Transacao(Guid.NewGuid(),tran.TipoTransacao, tran.Valor, tran.Descricao));
			}
			return transacaos;
		}

		private static List<TransacaoEntity> mapearTransacoes(List<Transacao> trans) {
			List<TransacaoEntity> transacaos = new List<TransacaoEntity>();
			foreach (var tran in trans)
			{
				transacaos.Add(new TransacaoEntity() {
					Descricao = tran.Descricao,
					Valor = tran.Valor,
					TipoTransacao = tran.TipoTransacao,
					Id = tran.IdTransacao
				});
			}
			return transacaos;
		}

		public ContaCorrente Load(Guid id) {
			return (from c in this.Contas.Include("Transacoes") where c.Id == id select new ContaCorrente(c.Id, c.IdCliente, mapearTransacoes(c.Transacoes))).FirstOrDefault();
		}

		public List<ContaCorrente> List(){
			return  (from c in this.Contas.Include("Transacoes") select new ContaCorrente(c.Id, c.IdCliente, mapearTransacoes(c.Transacoes))).ToList();
		}

		public void Save(ContaCorrente conta){
			ContaCorrenteEntity contaBanco = this.Contas.FirstOrDefault(c => c.Id == conta.IdConta);

			if (contaBanco != null) {
				// Já existe
				contaBanco.Transacoes = mapearTransacoes(conta.Extrato);
				foreach(var tran in contaBanco.Transacoes) {
					this.Transacoes.Add(tran);
				}
				this.Contas.Update(contaBanco);
			} else {
				this.Contas.Add(new ContaCorrenteEntity() {
					Id = conta.IdConta,
					IdCliente = conta.IdCliente,
					Transacoes = mapearTransacoes(conta.Extrato)
				});
			}
			this.SaveChanges();
		}

		public void Delete(ContaCorrente conta){
			this.Contas.Remove(new ContaCorrenteEntity() {Id =  conta.IdConta});
			this.SaveChanges();
		}
		
		public List<ContaCorrente> ListByCustomer(Guid customerId){
			return (from c in this.Contas.Include("Transacoes") where c.IdCliente == customerId select new ContaCorrente(c.Id, c.IdCliente, mapearTransacoes(c.Transacoes))).ToList();
		}
	}
}