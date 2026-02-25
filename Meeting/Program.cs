using System;
using System.Collections.Generic;

class PlaylistManager
{
    private LinkedList<string> playlist = new LinkedList<string>();

    public void addSong(string songId)
    {
        if (!playlist.Contains(songId))
            playlist.AddLast(songId);
    }

    public void removeSong(string songId)
    {
        playlist.Remove(songId);
    }

    public void moveToTop(string songId)
    {
        if (playlist.Contains(songId))
        {
            playlist.Remove(songId);
            playlist.AddFirst(songId);
        }
    }

    public string getPlaylistOrder()
    {
        if (playlist.Count == 0)
            return "Empty Playlist";

        return string.Join(" ", playlist);
    }

    public void processCommands(int N)
    {
        for (int i = 0; i < N; i++)
        {
            string input = Console.ReadLine();
            string[] parts = input.Split(' ');

            string command = parts[0];

            if (command == "ADD")
                addSong(parts[1]);

            else if (command == "REMOVE")
                removeSong(parts[1]);

            else if (command == "TOP")
                moveToTop(parts[1]);

            else if (command == "PRINT")
                Console.WriteLine(getPlaylistOrder());
        }
    }
}

class Program
{
    public static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        PlaylistManager manager = new PlaylistManager();
        manager.processCommands(N);
    }
}