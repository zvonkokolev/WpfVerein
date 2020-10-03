using System;
using System.Windows;
using WpfVerein.Model;

namespace WpfVerein.CRUD
{
	/// <summary>
	/// Interaktionslogik für AddMemberWindow.xaml
	/// </summary>
	public partial class AddMemberWindow : Window
	{
		private Member newCd;
		private static int _indexer = 14;
		private readonly Member _memberToEdit;

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
