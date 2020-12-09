using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjetoWPF_EventosDelegantesLambda
{
    //criando delegate
    //EventHandler - sufixo adicional de conveção do .net
    public delegate void ValidacaoEventHandler(object sender, ValidacaoEventArgs e);

    public class ValidacaoTextBox : TextBox
    {
        //cria um evento personalizado
        private ValidacaoEventHandler _validacao;
        public event ValidacaoEventHandler MinhaValidacao
        {
            add
            {
                _validacao += value;
                OnValidacao();
            }
            remove
            {
                _validacao -= value;
            }
        }

        //sobrescrevendo 
        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            base.OnTextChanged(e);
            OnValidacao();
        }

        protected virtual void OnValidacao()
        {
            //evita erro de validacao antes dos campos serem populados
            if (_validacao != null)
            {
                //guarda todos os delegates do tipo EventHandler
                var listaValidacao = _validacao.GetInvocationList();

                //constroi o argumento da validacao
                var eventArgs = new ValidacaoEventArgs(Text);

                var ehValido = true;

                //loop entre todas as validacoes
                foreach (ValidacaoEventHandler validacao in listaValidacao)
                {
                    //chama o eventArgs
                    validacao(this, eventArgs);

                    //se uma das validações retorna falso
                    if (!eventArgs.EhValido)
                    {
                        ehValido = false;
                        break;
                    };
                }

                if (ehValido)
                    Background = new SolidColorBrush(Colors.White);
                else
                    Background = new SolidColorBrush(Colors.Orange);
            }
        }
    }
}
