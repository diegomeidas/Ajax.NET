using Core.Info;
using Core.Filtro;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace Core.Dal
{
    class DalProduto
    {
        private DataBaseClass _objDB;
        private DefProduto _objDef;
        private DataTable _dtProduto;
        private SqlCommand cmd;

        public DalProduto()
        {
            _objDB = new DataBaseClass();
            _objDef = new DefProduto();

        }

        public DataTable Table
        {
            get { return _dtProduto; }
            set { _dtProduto = value; }
        }


        public InfProduto Inserir(InfProduto objInfo)
        {
            //abre conexao 
            cmd = new SqlCommand();
            cmd.Connection = _objDB.Conectar();
            try {
                
                cmd.CommandType = CommandType.Text;
               
                string insert = "INSERT INTO Products (codigo, nome) VALUES(@codigo, @nome)";
                cmd.CommandText = insert;
                cmd.Parameters.Add("@codigo", SqlDbType.Int).Value = objInfo.Codigo;
                cmd.Parameters.Add("@nome", SqlDbType.VarChar).Value = objInfo.Nome;
                
                int id = 0;
                if (int.TryParse(cmd.ExecuteScalar().ToString(), out id))
                {
                    objInfo.Id = id;
                }
                return objInfo;
            }
           

            ////parâmetros da stored procedure 
            //SqlParameter pcodigo = new SqlParameter("@codigo", SqlDbType.Int);
            //pcodigo.Direction = ParameterDirection.Output;
            //cmd.Parameters.Add(pcodigo);

            //SqlParameter pnome = new SqlParameter("@nome", SqlDbType.NVarChar, 100);
            //pnome.Value = objInfo.Nome;
            //cmd.Parameters.Add(pnome);

            //SqlParameter pemail = new SqlParameter("@email", SqlDbType.NVarChar, 100);
            //pemail.Value = objInfo.Email;
            //cmd.Parameters.Add(pemail);

            //SqlParameter ptelefone = new SqlParameter("@telefone", SqlDbType.NVarChar, 80);
            //ptelefone.Value = objInfo.Telefone;
            //cmd.Parameters.Add(ptelefone);

            //cn.Open(); cmd.ExecuteNonQuery();
            //objInfo.Codigo = (Int32)cmd.Parameters["@codigo"].Value;




        
            catch (SqlException ex)
            {
                throw new Exception("Servidor SQL Erro:" + ex.Number);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                _objDB.Close();
            }
        }

        public void Inserir(InfProduto objInfo)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "INSERT INTO tb_wiser_evento_chamado(evc_id,";
            _txtSQL.SQL = "wch_id,";
            _txtSQL.SQL = "wch_situacao,";
            _txtSQL.SQL = "wch_auto)";                                              
            _txtSQL.SQL = "VALUES (";
            _txtSQL.SQL = Converter.IsStrNull(objInfo.IdClienteEvento) + ",";
            _txtSQL.SQL = Converter.IsStrNull(objInfo.Id) + ",";
            _txtSQL.SQL = Converter.IsStrNull(objInfo.Situacao) + ",";
            _txtSQL.SQL = Converter.IsStrNull(objInfo.Auto) + ")";                  
            DataTable dtIns = new DataTable();
            _objDB.CommandType = CommandType.Text;
            _objDB.CommandText = _txtSQL.SQL;
            if (_objDB.ExecutarSQL() == false)
            {
                throw new Exception(_objDB.DescricaoErro);
            }
        }

        public void Alterar(InfWiserEventoChamado objInfo)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "UPDATE tb_wiser_evento_chamado SET";
            _txtSQL.SQL = "evc_id = " + Converter.IsStrNull(objInfo.IdClienteEvento) + ",";
            _txtSQL.SQL = "wch_id = " + Converter.IsStrNull(objInfo.Id) + ",";
            _txtSQL.SQL = "wch_situacao = " + Converter.IsStrNull(objInfo.Situacao) + ",";
            _txtSQL.SQL = "wch_auto = " + Converter.IsStrNull(objInfo.Auto);               //---obs
            _txtSQL.SQL = "WHERE evc = " + objInfo.IdClienteEvento.ToString();
            _objDB.CommandType = CommandType.Text;
            _objDB.CommandText = _txtSQL.SQL;
            if (_objDB.ExecutarSQL() == false)
            {
                throw new Exception(_objDB.DescricaoErro);
            }
        }

        public void Excluir(InfWiserEventoChamado objInfo)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "DELETE FROM tb_wiser_evento_chamado";
            _txtSQL.SQL = "WHERE evc = " + objInfo.IdClienteEvento;
            _objDB.CommandType = CommandType.Text;
            _objDB.CommandText = _txtSQL.SQL;
            if (_objDB.ExecutarSQL() == false)
            {
                throw new Exception(_objDB.DescricaoErro);
            }
        }

        public void Consultar(FilWiserEventoChamado objFil)
        {
            if (objFil.SQL == SqlWiserEventoChamado.Padrao)
            {
                setSQLPadrao(objFil);
            }
            else if (objFil.SQL == SqlWiserEventoChamado.Completo)
            {
                setSQLCompleto(objFil);
            }
            else if (objFil.SQL == SqlWiserEventoChamado.Evento)
            {
                setSQLEvento(objFil);
            }
            else if (objFil.SQL == SqlWiserEventoChamado.AlteraObservacao)
            {
                setSQLAlteraObservacao(objFil);
            }
            _objDB.CommandType = CommandType.Text;
            _objDB.CommandText = _txtSQL.SQL;
            if (_objDB.ExecutarQuery(out _dtWiserEventoChamado) == false)
            {
                throw new Exception(_objDB.DescricaoErro);
            }
        }

        private void setSQLPadrao(FilWiserEventoChamado objFil)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "SELECT wch.* ";
            _txtSQL.SQL = "FROM tb_wiser_evento_chamado wch";
            setSQLFiltros(objFil);
            _txtSQL.SQL = "ORDER BY wch_id";
        }
        private void setSQLCompleto(FilWiserEventoChamado objFil)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "select wev.*,wch.wch_codigo";
            _txtSQL.SQL = "from tb_wiser_chamado wch";
            _txtSQL.SQL = "inner join tb_wiser_evento_chamado wev on wch.wch_Id = wev.wch_Id";
            setSQLFiltros(objFil);
            _txtSQL.SQL = "order by wch_id";
        }
        private void setSQLEvento(FilWiserEventoChamado objFil)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "select *";
            _txtSQL.SQL = "from tb_wiser_evento_chamado wev";
            _txtSQL.SQL = "inner join tb_cliente_evento evc on evc.evc_id  = wev.evc_id";
            _txtSQL.SQL = "LEFT join vw_operador ope on ope.ope_id = evc.ope_id";
            setSQLFiltros(objFil);
            _txtSQL.SQL = "order by wev.evc_id desc";
        }
        private void setSQLAlteraObservacao(FilWiserEventoChamado objFil)
        {
            _txtSQL.Clear();
            _txtSQL.SQL = "select top 1 *";
            _txtSQL.SQL = "from tb_wiser_evento_chamado wev";
            _txtSQL.SQL = "inner join tb_cliente_evento evc on evc.evc_id  = wev.evc_idc";
            _txtSQL.SQL = "LEFT join vw_operador ope on ope.ope_id = evc.ope_id";
            setSQLFiltros(objFil);
            _txtSQL.SQL = "order by wev.evc_id desc";
        }
        private void setSQLFiltros(FilWiserEventoChamado objFil)
        {
            foreach (clsFiltro filtro in objFil.Filtros)
            {
                _txtSQL.AddWhere(filtro.SQL);
            }
        }
    }

    public enum SqlWiserEventoChamado
    {
        Padrao = 1,
        Completo = 2,
        Evento = 3,
        AlteraObservacao = 4
    }
}

    

