<Query Kind="Expression">
  <Connection>
    <ID>ba087d1f-172a-4f3d-9784-ef06bb5cbe1e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//Joins

//www.dotnetlearners.com/linq

//Rules:
// 1. if you have a navigational property between the tables
//    this should be your first choice of attack
// 2. if you do not have a navigational property then you can
//    do a join of your tables

//assume for this example that there is no navigational property
//between Artists and Albums

//the first table to be referenced should be the main processing data pile
//the other table(s) are the support tables to the first table

from x in Albums
join y in Artists on x.ArtistId equals y.ArtistId
select new
{
	Title = x.Title,
	Year = x.ReleaseYear,
	Label = x.ReleaseLabel == null ? "unknown" : x.ReleaseLabel,
	Artist = y.Name,
	trackcount = x.Tracks.Count()
}










