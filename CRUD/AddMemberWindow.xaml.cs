using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using WpfVerein.Model;
using WpfVerein.Utils;

namespace WpfVerein.CRUD
{
	/// <summary>
	/// Interaktionslogik für AddMemberWindow.xaml
	/// </summary>
	public partial class AddMemberWindow : Window
	{
		private CsvWriter _csvWriter;
		private string path = MyString.GetFullNameInApplicationTree("output.csv");
		private Member newCd;
		private static int _indexer = 14;
		private readonly Member _memberToEdit;
		private List<string> _line;
		public AddMemberWindow(Member memberToEdit)
		{
			_memberToEdit = memberToEdit;
			InitializeComponent();
		}
		public AddMemberWindow()
		{
			_indexer++;
			InitializeComponent();
		}
		private void AddMemberWindow_Loaded(object sender, RoutedEventArgs e)
		{
			if(_memberToEdit != null)
			{
				tbIndex.Text = _memberToEdit.Index.Value.ToString(); ;
				tbFirstname.Text = _memberToEdit.Firstname;
				tbLastname.Text = _memberToEdit.Lastname;
				tbEmail.Text = _memberToEdit.Email;
				tbPhone.Text = _memberToEdit.Phone;
				tbBirthday.Text = _memberToEdit.BirthDay.ToString();
			}
			newCd = new Member
			{
				Index = _indexer,
				Firstname = tbFirstname.Text,
				Lastname = tbLastname.Text,
				Email = tbEmail.Text,
				Phone = tbPhone.Text,
				BirthDay = DateTime.Now
			};

			grdCdField.DataContext = newCd;
		}

		private void BtnSave_Clicked(object sender, RoutedEventArgs e)
		{
			if (_memberToEdit == null)
			{
				Repository.GetInstance().AddMember(newCd);
				_line = new List<string>
				{
					newCd.Name,
					newCd.Email,
					newCd.Phone
				};
				// if member is new one, will be appended into csv file
				_csvWriter = new CsvWriter(path, ";");
				_csvWriter.Write(_line.ElementAt(0), _line.ElementAt(1), _line.ElementAt(2));
				_csvWriter.Flush();
			}
			else
			{
				Repository.GetInstance().UpdateCd(_memberToEdit, newCd);
			}
			Close();
		}

		private void BtnCancel_Clicked(object sender, RoutedEventArgs e)
		{
			Close();
		}
	}
}
