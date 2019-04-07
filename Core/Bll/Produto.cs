using Core.Dal;
using Core.Filtro;
using Core.Info;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Bll
{
    public class Produto
    {
        private DalProduto _objDal;
        private InfProduto _objInf;
        private FilProduto _objFil;

        public Produto()
        {
            InicializaClasse();
        }

        private void InicializaClasse()
        {
            _objDal = new DalProduto();
            _objInf = new InfProduto();
            _objFil = new FilProduto();
        }

        public InfProduto Info
        {
            get { return _objInf; }
            set { _objInf = value; }
        }

        public FilProduto Filtro
        {
            get { return _objFil; }
            set { _objFil = value; }
        }

        protected void ValidaFieldsObrigatorios()
        {
        }

        public void Salvar()
        {
            ValidaFieldsObrigatorios();
            _objDal.Inserir(_objInf);
        }
        public void Alterar()
        {
            ValidaFieldsObrigatorios();
            _objDal.Alterar(_objInf);
        }

        public void Excluir()
        {
            _objDal.Excluir(_objInf);
        }

        public void Excluir(List<int> list)
        {
            foreach (int Id in list)
            {
                _objInf.Id = Id;
                this.Excluir();
            }
        }

        public void Consultar()
        {
            _objDal.Consultar(_objFil);
            if (_objDal.Table.Rows.Count == 1)
            {
                _objInf.DataRowToInfo(_objDal.Table.Rows[0]);
            }
        }

        public DataTable Table
        {
            get { return _objDal.Table; }
        }

        public bool IsEmpty
        {
            get { return (_objDal.Table.Rows.Count == 0); }
        }

        public List<InfProduto> GetList()
        {
            List<InfProduto> list = new List<InfProduto>();
            foreach (DataRow row in _objDal.Table.Rows)
            {
                InfProduto info = new InfProduto(row);
                list.Add(info);
            }
            return list;
        }
        


    }//fim class
}
