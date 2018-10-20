<Query Kind="Program">
  <Connection>
    <ID>ba087d1f-172a-4f3d-9784-ef06bb5cbe1e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

void Main()
{
	//Create a list of albums showing its title and artist.
	//Show albums with 25 or more tracks only.
	//For each album show the songs on the album and their length.
	
	var results = from x in Albums
				  where x.Tracks.Count() > 24
				  select new AnAlbum
				{
					artist = x.Artist.Name,
					title = x.Title,
					songs = (from y in x.Tracks
					        select new Song
							{
								songname = y.Name,
								length = y.Milliseconds/60000 + ":" +
								           (y.Milliseconds%60000)/1000
							}).ToList()
				};
	//results.Dump();
	
	//create a list of employees and the client they support
	var employeelist = from x in Employees
						where x.Title.Contains("Support")
						orderby x.LastName, x.FirstName
						select new SupportEmployee
						{
							Name = x.LastName + ", " + x.FirstName,
							ClientCount = x.SupportRepIdCustomers.Count(),
							ClientList = (from y in x.SupportRepIdCustomers
										 orderby y.LastName, y.FirstName
										 select new PlayListCustomer
										{
											lastname = y.LastName,
											firstname = y.FirstName,
											phone = y.Phone
										}).ToList()
						};
	employeelist.Dump();
}

// Define other methods and classes here
//Song is the POCO
public class Song
{
 	public string songname{get;set;}
 	public string length{get;set;}
}

public class PlayListCustomer
{
	public string lastname {get;set;}
	public string firstname {get;set;}
	public string phone {get;set;}
}
//AnAlbum is the DTO. It has structure (a set of data on each instance
//   of the class.
public class AnAlbum
{
	public string artist{get;set;}
	public string title{get;set;}
	public List<Song> songs{get;set;}
}

public class SupportEmployee
{
	public string Name{get;set;}
	public int ClientCount{get;set;}
	public List<PlayListCustomer> ClientList{get;set;}
}