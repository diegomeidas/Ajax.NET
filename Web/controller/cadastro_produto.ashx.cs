using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Web.controller
{
    /// <summary>
    /// Descrição resumida de cadastro_produto
    /// </summary>
    public class cadastro_produto : IHttpHandler
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();


        public void ProcessRequest(HttpContext context)
        {
            string response = string.Empty;

            string metodo = Convert.ToString(context.Request.QueryString["metodo"]);
            string retorno = string.Empty;
            try
            {
                switch (metodo)
                {
                    case "localizacliente":
                        context.Response.ContentType = "text/json";
                        string cpf = context.Request.QueryString["cpf"];
                        retorno = localizaCliente(cpf);
                        break;
                    case "getdadospessoa":
                        context.Response.ContentType = "text/json";
                        retorno = getDadosClienteResumo();
                        break;
                    case "getdadoscliente":
                        context.Response.ContentType = "text/json";
                        retorno = getDadosCliente();
                        break;
                    case "enviacodigo":
                        context.Response.ContentType = "text";
                        string tipo = context.Request.QueryString["tipo"];
                        string telefone = context.Request.QueryString["telefone"];
                        string email = context.Request.QueryString["email"];
                        string idpessoa = context.Request.QueryString["idpessoa"];
                        retorno = EnviaCodigo(tipo, telefone, email, idpessoa);
                        break;
                    case "setdadoscliente":
                        context.Response.ContentType = "text";
                        string cliente = context.Request.Form["objeto"];
                        retorno = setCliente(cliente);
                        break;
                    case "setemailcliente":
                        context.Response.ContentType = "text";
                        email = context.Request.QueryString["email"];
                        idpessoa = context.Request.QueryString["idcliente"];
                        retorno = setEmailCliente(email, idpessoa);
                        break;
                    case "settelefonecliente":
                        context.Response.ContentType = "text";
                        telefone = context.Request.QueryString["telefone"];
                        idpessoa = context.Request.QueryString["idcliente"];
                        retorno = setTelefoneCliente(telefone, idpessoa);
                        break;
                    case "listuf":
                        context.Response.ContentType = "text/json";
                        retorno = ListUF();
                        break;
                    case "listcidade":
                        context.Response.ContentType = "text/json";
                        string iduf = context.Request.QueryString["iduf"];
                        retorno = ListCidades(iduf);
                        break;
                }
            }
            catch (Exception e)
            {

            }
            context.Response.Write(retorno);
        }

        private string ListCidades(string iduf)
        {
            throw new NotImplementedException();
        }

        private string ListUF()
        {
            throw new NotImplementedException();
        }

        private string setTelefoneCliente(string telefone, string idpessoa)
        {
            throw new NotImplementedException();
        }

        private string setEmailCliente(string email, string idpessoa)
        {
            throw new NotImplementedException();
        }

        private string setCliente(string cliente)
        {
            throw new NotImplementedException();
        }

        private string EnviaCodigo(string tipo, string telefone, string email, string idpessoa)
        {
            throw new NotImplementedException();
        }

        private string getDadosCliente()
        {
            throw new NotImplementedException();
        }

        private string getDadosClienteResumo()
        {
            throw new NotImplementedException();
        }

        private string localizaCliente(string cpf)
        {
            string result;
            //chama a dal
            result = serializer.Serialize(dal.ConsultarClientePortalPorCPFCNPJ(cpf));
            return result;

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        
    }
}