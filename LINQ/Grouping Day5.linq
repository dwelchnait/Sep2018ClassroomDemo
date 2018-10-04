<Query Kind="Expression">
  <Connection>
    <ID>ba087d1f-172a-4f3d-9784-ef06bb5cbe1e</ID>
    <Persist>true</Persist>
    <Server>.</Server>
    <Database>Chinook</Database>
  </Connection>
</Query>

//grouping of data within itself
//a) by column
//b) by multiple column
//c) by an entity
//
//grouping can be save temporarily into
//  a dataset and that dataset can be report on
//  
//the grouping attribute is referred to as the .Key
//multiple attributes or entity use .Key.attribute

//report albums by ReleaseYear
from x in Albums
group x by x.ReleaseYear into gYear
select gYear

from x in Albums
group x by x.ReleaseYear into gYear
select new
		{
			year = gYear.Key,
			numberofalbumsforyear = gYear.Count(),
			albumandartist = from y in gYear
								select new
								{
									title = y.Title,
									artist = y.Artist.Name,
									albumsongcount = y.Tracks.Count()
								}
		}
		
//grouping of tracks by Genre Name
//actions against your data BEFORE grouping
// is done against the natural entity attribute
//actions done AFTER  grouping MUST refer to the
//  temporary group dataset
//grouping can be done against a complete Entity
//this type of grouping produces a KEY set of ALL
//    Entity attributes
from t in Tracks
where t.Album.ReleaseYear > 2010
group t by t.Genre into gTemp
orderby gTemp.Count() descending
select new 
		{
			genre = gTemp.Key.Name,
			numberof = gTemp.Count()
		}


		
		
		
		
		
		
		
		
		