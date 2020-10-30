using System;

namespace hexagonal_ddd.Core.Domain
{

    public class Transacao
    {
		
		public Transacao(Guid Id, TipoTransacao tipoTransacao, int valor, string descricao) {
			this.IdTransacao = Id;
			this.TipoTransacao = tipoTransacao;
			this.Valor = valor;
			this.Descricao = descricao;
		}

		public Guid IdTransacao {
			get;
		}

		public TipoTransacao TipoTransacao {
			get; 
		}

		public int Valor {
			get; 
		}
		
		public string Descricao {
			get; 
		}
		

    }
}