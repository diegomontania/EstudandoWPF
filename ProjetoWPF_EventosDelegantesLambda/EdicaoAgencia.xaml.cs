using ByteBank.Agencias.DAL;
using System;
using System.Windows;

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
            //RoutedEventHandler dialogResultTrue = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = true;
            //};

            //RoutedEventHandler dialogResultFalse = delegate (object o, RoutedEventArgs e)
            //{
            //    DialogResult = false;
            //};

            //mesmo método anônimo que o anterior, porém usando expressão lambda
            RoutedEventHandler dialogResultTrue = (o, e) => DialogResult = true;
            RoutedEventHandler dialogResultFalse = (o, e) => DialogResult = false;

            var okEventHandler = dialogResultTrue + Fechar;
            var cancelarEventHandler = dialogResultFalse + Fechar;

            btnOk.Click += okEventHandler;
            btnCancelar.Click += cancelarEventHandler;
        }

        private void Fechar(object sender, EventArgs e) =>
            Close();
    }
}
