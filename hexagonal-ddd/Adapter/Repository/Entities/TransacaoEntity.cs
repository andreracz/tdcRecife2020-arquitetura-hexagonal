using System;
using hexagonal_ddd.Core.Domain;

namespace hexagonal_ddd.Adapter.Repository.Entities
{

    public class TransacaoEntity
    {
		
		public TransacaoEntity() {
		}

		public TipoTransacao TipoTransacao {
			get; set;
		}

		public int Valor {
			get; set; 
		}
		
		public string Descricao {
			get; set;
		}

		public Guid Id {
			get; set;
		}

		public Guid ContaCorrenteEntityId {
			get; set;
		}
		

    }
}