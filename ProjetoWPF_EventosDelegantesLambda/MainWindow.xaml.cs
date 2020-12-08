using ByteBank.Agencias.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjetoWPF_EventosDelegantesLambda
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //faz conexão com o banco
        private readonly ByteBankEntities _contextoBancoDeDados = new ByteBankEntities();
        private readonly ListBox _lstAgencias;

        public MainWindow()
        {
            InitializeComponent();

            _lstAgencias = new ListBox();
            
            AtualizarControles();
            AtualizarListaDeAgencias();
        }

        private void AtualizarControles()
        {
            //definindo propriedades do objeto ListView
            _lstAgencias.Width = 270;
            _lstAgencias.Height = 290;
            Canvas.SetTop(_lstAgencias, 15);
            Canvas.SetLeft(_lstAgencias, 15);

            //adiciona ao form o objeto de lista
            container.Children.Add(_lstAgencias);

            //adiciona os dados ao list
            _lstAgencias.SelectionChanged += new SelectionChangedEventHandler(lstAgencias_SelectionChanged);

            btnEditar.Click += new RoutedEventHandler(btnEditar_Click);
        }

        private void AtualizarListaDeAgencias()
        {
            _lstAgencias.Items.Clear(); //limpa lista antes de inserir os dados

            //recebe os dados do _contextoBancoDeDados e transforma em lista
            var agencias = _contextoBancoDeDados.Agencias.ToList();

            //itera pela lista e intera objeto list
            foreach (var agencia in agencias)
            {
                _lstAgencias.Items.Add(agencia);
            }
        }

        private void lstAgencias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var agenciaSelecionada = (Agencia)_lstAgencias.SelectedItem;

            txtNumero.Text = agenciaSelecionada.Numero;
            txtNome.Text = agenciaSelecionada.Nome;
            txtTelefone.Text = agenciaSelecionada.Telefone;
            txtEndereco.Text = agenciaSelecionada.Endereco;
            txtDescricao.Text = agenciaSelecionada.Descricao;
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            //recupera a agencia atual
            var agenciaAtual = (Agencia)_lstAgencias.SelectedItem;

            //instancia a janela
            var janelaEdicao = new EdicaoAgencia(agenciaAtual);
            var resultado = janelaEdicao.ShowDialog().Value;

            if (resultado)
            {   
                //usuario clicou ok
            }
            else 
            {
                //usuario clicou cancelar
            }
        }

        //'Sinal de raio' = evento. Ou seja, um evento externo
        private void btnExcluir_Click(object sender, RoutedEventArgs e)
        {
            var confirmacaoUsuario = 
                MessageBox.Show("Deseja realmente excluir?", "Confirmação", MessageBoxButton.YesNo);

            if (confirmacaoUsuario == MessageBoxResult.Yes)
            {
                //usuario yes
            }
            else
            {
                //usuario no
            }
        }
    }
}
