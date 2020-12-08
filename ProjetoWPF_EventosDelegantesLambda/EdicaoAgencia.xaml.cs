using ByteBank.Agencias.DAL;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ProjetoWPF_EventosDelegantesLambda
{
    /// <summary>
    /// Interaction logic for EdicaoAgencia.xaml
    /// </summary>
    public partial class EdicaoAgencia : Window
    {
        private readonly Agencia _agencia;

        public EdicaoAgencia(Agencia agencia)
        {
            InitializeComponent();

            //se for nulo retorne com exception
            _agencia = agencia ?? throw new ArgumentException(nameof(agencia));

            AtualizarCamposDeTexto();
            AtualizarControles();
        }

        private void AtualizarCamposDeTexto()
        {
            txtNumero.Text = _agencia.Numero;
            txtNome.Text = _agencia.Nome;
            txtTelefone.Text = _agencia.Telefone;
            txtEndereco.Text = _agencia.Endereco;
            txtDescricao.Text = _agencia.Descricao;
        }

        //combinando e adicionando comportamentos aos botoes usando delegates
        private void AtualizarControles()
        {
            //cria um método anônimo, utilizando RoutedEventHandler que é utilizado em pequeno escopo
            //o resultado do método já ficará armazenado diretamente na variavel 'dialogResultTrue'
            #region RoutedEventHandler
            //RoutedEventHandler dialogResultTrue = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = true;
            //};

            //RoutedEventHandler dialogResultFalse = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = false;
            //};
            #endregion

            //mesmo método anônimo que o anterior, porém usando expressão lambda
            RoutedEventHandler dialogResultTrue = (o, e) => DialogResult = true;
            RoutedEventHandler dialogResultFalse = (o, e) => DialogResult = false;

            var okEventHandler = dialogResultTrue + Fechar;
            var cancelarEventHandler = dialogResultFalse + Fechar;

            //associando objetos do form aos seus metodos
            btnOk.Click += okEventHandler;
            btnCancelar.Click += cancelarEventHandler;

            txtNome.TextChanged += ValidarCampoNulo;
            txtDescricao.TextChanged += ValidarCampoNulo;
            txtEndereco.TextChanged += ValidarCampoNulo;
            txtNumero.TextChanged += ValidarCampoNulo;
            txtTelefone.TextChanged += ValidarCampoNulo;
        }

        #region Metodo que retorna um delegate
            //utilizacao
            //txtNome.TextChanged += ConstruirDelegateValidacaoCampoNulo(txtNome);
            //private TextChangedEventHandler ConstruirDelegateValidacaoCampoNulo(TextBox txt)
            //{
            //    //usando expressão lambda para o retorno
            //    return (o, e) =>
            //    {
            //        var textoEstaVazio = String.IsNullOrEmpty(txtNome.Text);

            //        if (textoEstaVazio)
            //            txtNome.Background = new SolidColorBrush(Colors.OrangeRed);
            //        else
            //            txtNome.Background = new SolidColorBrush(Colors.White);
            //    };
            //}
            #endregion

        //metodo que retorna um delegate
        //se o campo estiver vazio mude a cor para laranja
        private void ValidarCampoNulo(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            var textoEstaVazio = String.IsNullOrEmpty(txt.Text);

            if (textoEstaVazio)
                txt.Background = new SolidColorBrush(Colors.OrangeRed);
            else
                txt.Background = new SolidColorBrush(Colors.White);
        }

        private void Fechar(object sender, EventArgs e) =>
            Close();
    }
}
