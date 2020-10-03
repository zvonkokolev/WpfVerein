using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfVerein.CRUD;
using WpfVerein.Model;
using WpfVerein.Utils;

namespace WpfVerein
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private List<Member> _members;
		private Repository rep;
		private CsvWriter _csvWriter;
		private string path = MyString.GetFullNameInApplicationTree("output.csv");

		public MainWindow()
		{
			InitializeComponent();
			Loaded += new RoutedEventHandler(MainWindow_Loaded);
		}

		private void MainWindow_Loaded(object sender, RoutedEventArgs e)
		{
			using (var db = new ClubMemberContext())
			{
				rep = Repository.GetInstance();
				_members = rep.GetAllMembers();
				lbxCds.ItemsSource = _members;
				// prints members data to csv file
				_csvWriter = new CsvWriter(path, ";");
				for (int i = 0; i < _members.Count; i++)
				{
					_csvWriter.Write
						(
						_members.ElementAt(i).Name,
						_members.ElementAt(i).Email,
						_members.ElementAt(i).Phone
						);
					// db.Add(_members.ElementAt(i));
				}
				// Read and save into database
				db.AttachRange(_members);
				db.SaveChanges();
			}
		}

		private void BtnMainWindow_Clicked(object sender, RoutedEventArgs e)
		{  // saves data into csv file
			_csvWriter.Flush();
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
				// Database---
				using (var db = new ClubMemberContext())
				{
					if (selectedCd == null)
					{
						MessageBox.Show("Wählen Sie einen Mitglied!");
					}
					else
					{              // delete member
						if (button.Name.Equals("btnDel"))
						{
							rep.RemoveCd(selectedCd);
							// Delete--------------------------
							MessageBox.Show("Lösche Mitglied aus der Datenbank");
							db.Remove(selectedCd);
							db.SaveChanges();
							//---------------------------------
						}           // edit member
						else if (button.Name.Equals("btnEdit"))
						{
							AddMemberWindow addMemberWindow = new AddMemberWindow(selectedCd);
							addMemberWindow.ShowDialog();
						}
					}
				}
			}
			_members = rep.GetAllMembers();
			lbxCds.ItemsSource = _members;
		}
	}
}
