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
	//list all the Playlists which have at least a track.
	//Show the playlist name, number of playlist tracks,
	//the cost of the playlist and the total storage size for
	//the playlist.
	
	var results9 = from x in Playlists
					where x.PlaylistTracks.Count() > 0
					select new PlaylistSummary
					{
						name= x.Name,
						trackcount = x.PlaylistTracks.Count(),
						cost = x.PlaylistTracks.Sum(plt => plt.Track.UnitPrice),
						storage = x.PlaylistTracks.Sum(plt => plt.Track.Bytes/1000.0)
					};
	results9.Dump();
}

// Define other methods and classes here
public class PlaylistSummary
{
	public string name{get;set;}
	public int trackcount{get;set;}
	public decimal cost{get;set;}
	public double? storage{get;set;}
	
}









