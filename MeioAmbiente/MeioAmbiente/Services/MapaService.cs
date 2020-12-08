using MeioAmbiente.Models;
using Plugin.ExternalMaps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeioAmbiente.Services
{
    public class MapaService
    {
        public static Pesquisa PoeEnderecoMapa (Pesquisa pesquisa)
        {
            string Pais = "BR";
            string CodigoPais = "55";

            try
            {
                 CrossExternalMaps.Current.NavigateTo("Teste", pesquisa.Logradouro, pesquisa.Localidade, pesquisa.Uf,pesquisa.Cep,Pais,CodigoPais);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return null;
            
        }
    }
}
