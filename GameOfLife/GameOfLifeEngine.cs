﻿namespace GameOfLife;

public class GameOfLifeEngine
{
    private readonly HashSet<Cell> Cells = new();

    private List<HashSet<Cell>> CellIterations = new List<HashSet<Cell>>();

    public void Update()
    {
        var oldCells = new HashSet<Cell>();
        foreach (var cell in Cells) oldCells.Add(cell);
        CellIterations.Add(oldCells);

        var cellsForDeleting = new List<Cell>();
        var newCells = new List<Cell>();

        foreach (var cell in Cells)
        {
            if (GetCountOfAliveNeighbours(cell) is < 2 or > 3)
                cellsForDeleting.Add(cell);

            foreach (var neighbour in cell.GetNeighbourCells())
                if (GetCountOfAliveNeighbours(neighbour) == 3)
                    newCells.Add(neighbour);
        }

        foreach (var cellForDeleting in cellsForDeleting) Remove(cellForDeleting);
        Add(newCells);
    }

    public void GetBack()
    {
        if (CellIterations.Count() == 0) return;
        var lastEngine = CellIterations[CellIterations.Count()-1];
        CellIterations.RemoveAt(CellIterations.Count()-1);
        Cells.Clear();
        Add(lastEngine.ToList());
    }

    public int GetCountOfAliveNeighbours(Cell cell)
    {
        var count = 0;
        foreach (var neighbour in cell.GetNeighbourCells())
            if (Contains(neighbour))
                count++;

        return count;
    }

    public void Add(List<Cell> cells)
    {
        foreach (var cell in cells) Cells.Add(cell);
    }

    public bool Contains(Cell cell)
    {
        return Cells.Contains(cell);
    }

    public List<Cell> GetCells()
    {
        return Cells.ToList();
    }

    public void Remove(Cell cell)
    {
        Cells.Remove(cell);
    }
}