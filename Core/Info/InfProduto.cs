using Core.View;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Info
{
        public class InfProduto
        {
            
            private int _intId;
            private int _intCodigo;
            private string _strNome;
            private double 
            //private Fornecedor _fornecedor;

            public InfProduto()
            {
                Clear();
            }

            public InfProduto(DataRow row)
            {
                Clear();
                DataRowToInfo(row);
            }

            public virtual void Clear()
            {
                
                _intId = 0;
                _intCodigo = 0;
                _strNome = null;
                //Fornecedor = new Fornecedor();
            }

            
            public int Id
            {
                get { return _intId; }
                set { _intId = value; }
            }

            public int Codigo
            {
                get { return _intCodigo; }
                set { _intCodigo = value; }
            }

            //public Fornecedor Fornecedor
            //{
            //    get { return _fornecedor; }
            //    set { _fornecedor = value; }
            //}

            public string Nome
            {
                get { return _strNome; }
                set { _strNome = value; }
            }

            public void DataRowToInfo(DataRow row)
            {
                DefProduto objDef = new DefProduto();
                this.IdClienteEvento = Convert.ToInt32(objDef.IdClienteEvento, row);
                this.Id = Convert.ToInt32(objDef.Id, row);
                this.Situacao = Convert.ToInt32(objDef.Situacao, row);
                this.Auto = Convert.ToBoolean(objDef.Auto, row);
                VwProduto.DataRowToInfo(row);
            }

        }

       
        
    
}
