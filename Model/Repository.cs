using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WpfVerein.Utils;

namespace WpfVerein.Model
{
	/// <summary>
	/// Repository als Singleton, damit die Daten aus dem CSV-File nur einmal gelesen werden!
	/// </summary>
	public class Repository
	{
		private const string fileName = "Verein.csv";

		private static Repository instance;

		List<Member> members;

		private Repository()
		{
			members = new List<Member>();
			LoadCdsFromCsv();
		}

		public static Repository GetInstance()
		{
			if (instance == null)
				instance = new Repository();

			return instance;
		}

		/// <summary>
		/// Lädt die Daten vom csv-File in die Collection
		/// </summary>
		private void LoadCdsFromCsv()
		{
			string[][] cdCsv = fileName.ReadStringMatrixFromCsv(true);
			members = cdCsv.Select(x =>
				 new Member
				 {
					 Index = int.Parse(x.ElementAt(0)),
					 Firstname = x.ElementAt(1),
					 Lastname = x.ElementAt(2),
					 Email = x.ElementAt(3),
					 Phone = x.ElementAt(4),
					 BirthDay = Convert.ToDateTime(x.ElementAt(5))
				 }).ToList();
		}

		/// <summary>
		/// Neuer Mitglied in der Verein einfügen
		/// </summary>
		/// <param name="member"></param>
		public void AddMember(Member member)
		{
			members.Add(member);
		}

		/// <summary>
		/// Liefert eine (neue!) Liste aller Mitglieder
		/// Entkoppelt die zurückgelieferte Collektion von der Collection im Repository
		/// Beachte! Die Objekte selbst sind jedoch noch dieselben (clonen wäre erforderlich)!
		/// </summary>
		/// <returns></returns>
		public List<Member> GetAllMembers()
		{
			return members.OrderBy(x => x.Firstname).ToList(); //Erstellt neue Liste!
		}

		public Member GetMemberByFirstAndLastname(string firstname, string lastname) =>
			members
				.Where(cd => cd.Firstname.Equals(firstname) && cd.Lastname.Equals(lastname))
				.FirstOrDefault();

		/// <summary>
		/// Löscht einen Mitglied
		/// </summary>
		/// <param name="member"></param>
		public void RemoveCd(Member member)
		{
			Member cdToDelete = members
				.Where(cd => cd.Firstname.Equals(member.Firstname) && cd.Lastname.Equals(member.Lastname))
				.FirstOrDefault();
			members.Remove(cdToDelete);
		}

		/// <summary>
		/// Bearbeitet einen Mitglied
		/// </summary>
		/// <param name="newMember"></param>
		public void UpdateCd(Member oldMember, Member newMember)
		{
			Member returnCd = members
				.Where(cd => cd.Index.Equals(oldMember.Index)
					&& cd.Firstname.Equals(oldMember.Firstname)
					&& cd.Lastname.Equals(oldMember.Lastname)
				)
				.FirstOrDefault()
				;
			returnCd.Lastname = newMember.Lastname;
			returnCd.Firstname = newMember.Firstname;
			returnCd.Index = newMember.Index;
		}
	}
}
