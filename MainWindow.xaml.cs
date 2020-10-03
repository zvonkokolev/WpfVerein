using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using WpfVerein.CRUD;
using WpfVerein.Model;

namespace WpfVerein
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Member> _members;
		private Repository rep;

		public MainWindow()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			rep = Repository.GetInstance();
			_members = rep.GetAllMembers();
			lbxCds.ItemsSource = _members;
		}

		private void BtnMainWindow_Clicked(object sender, RoutedEventArgs e)
		{
			Button button = sender as Button;
			rep = Repository.GetInstance();
			Member selectedCd = (Member)lbxCds.SelectedItem;
			// new member
			if (button.Name.Equals("btnNew"))
			{
				AddMemberWindow addMemberWindow = new AddMemberWindow();
				addMemberWindow.ShowDialog();
			}
			else
			{
				if (selectedCd == null)
				{
					MessageBox.Show("Wählen Sie einen Mitglied!");
				}
				else
				{					// delete member
					if (button.Name.Equals("btnDel"))
					{
						rep.RemoveCd(selectedCd);
					}           // edit member
					else if (button.Name.Equals("btnEdit"))
					{
						AddMemberWindow addMemberWindow = new AddMemberWindow(selectedCd);
						addMemberWindow.ShowDialog();
					}
				}
			}
			_members = rep.GetAllMembers();
			lbxCds.ItemsSource = _members;
		}
	}
}
