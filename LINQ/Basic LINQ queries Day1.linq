<Query Kind="Expression">
  <Connection>
    <ID>ba087d1f-172a-4f3d-9784-ef06bb5cbe1e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

// find all albums released between 2007 and 2010 inclusive
from x in Albums
where x.ReleaseYear >= 2007 &&
      x.ReleaseYear <= 2010
select x

// find all customers who are from the US, order by lastname then firstname
from x in Customers
where x.Country.Equals("USA")
orderby x.LastName, x.FirstName
select x

//find all US customers you have an email using yahoo. 
//Show only the customer full name and email
from x in Customers
where x.Country.Equals("USA") &&
      x.Email.Contains("yahoo")
select new
{
	FullName = x.LastName + ", " + x.FirstName,
	Email = x.Email
}

// Create an alphabetic list of albums showing album title and release year. Include
//the Artist name.
from x in Albums
orderby x.Title
select new
{
	Title = x.Title,
	Year = x.ReleaseYear,
	ArtistName = x.Artist.Name
}
//list the albums for U2, showing title and release year. Order by release year.
from x in Albums
where x.Artist.Name.Contains("U2")
orderby x.ReleaseYear
select new
{
	Title = x.Title,
	Year = x.ReleaseYear
}




