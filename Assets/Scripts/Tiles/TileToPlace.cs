using UnityEngine;

public class TileToPlace : Tile
{
    private bool _isBooked;

    public bool IsBooked => _isBooked;

    public void Booked()
    {
        _isBooked = true;
    }
}
