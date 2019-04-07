using Core.Info;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Filtro
{
    public class FilProduto : InfProduto
    {
        private DefWiserEventoChamado _objDef;
        private List<clsFiltro> _listCustom;
        private SqlWiserEventoChamado _tpSql;

        public FilWiserEventoChamado()
        {
            _objDef = new DefWiserEventoChamado();
            Clear();
        }

        public override void Clear()
        {
            base.Clear();
            _tpSql = SqlWiserEventoChamado.Padrao;
            _listCustom = new List<clsFiltro>();
        }

        public SqlWiserEventoChamado SQL
        {
            get { return _tpSql; }
            set { _tpSql = value; }
        }

        public List<clsFiltro> Filtros
        {
            get { return getFiltros(); }
        }

        public void Por(string propriedade, object valor)
        {
            try
            {
                Funcoes.SetValorObjeto(this, propriedade, valor);
            }
            catch
            {
                throw new Exception("Erro ao adicinar o filtro '" + propriedade + "' com o valor '" + valor + "'");
            }
        }

        private List<clsFiltro> getFiltros()
        {
            try
            {
                _objDef.Alias = (SQL != SqlWiserEventoChamado.Padrao);
                List<clsFiltro> list = Funcoes.GetListaFiltrosBase(this.GetType().BaseType, this, _objDef);
                list.AddRange(_listCustom);
                return list;
            }
            catch
            {
                throw new Exception("Erro ao recuperar os filtros da classe bases");
            }
        }
    }
}























}
