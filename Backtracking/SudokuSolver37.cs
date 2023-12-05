using System;
using System.Collections.Generic;

namespace Backtracking;

public class SudokuSolver37
{
    private readonly List<HashSet<int>> _rows = new ();
    private readonly List<HashSet<int>> _columns = new ();
    private readonly List<HashSet<int>> _boxes = new ();

    private bool _solved;

    private const char EmptyCell = '.';

    private int _length;

    public void SolveSudoku(char[][] board) 
    {
        _length = board.Length;

        InitializeSets();

        PopulateSets(board);
        
        Backtrack(0, 0, board);
    }

    private void PopulateSets(char[][] board)
    {
        for (var rowIndex = 0; rowIndex < _length; rowIndex++)
        {
            for (var columnIndex = 0; columnIndex < _length; columnIndex++)
            {
                var character = board[rowIndex][columnIndex];
                if (character == EmptyCell)
                {
                    continue;
                }
                
                var value = (int)char.GetNumericValue(character);
                
                _rows[rowIndex].Add(value);
                _columns[columnIndex].Add(value);
                _boxes[GetBoxId(rowIndex, columnIndex)].Add(value);
            }
        }
    }

    private void InitializeSets()
    {
        for (var row = 0; row < _length; row++)
        {
            _rows.Add(new HashSet<int>());
            _boxes.Add(new HashSet<int>());
            _columns.Add(new HashSet<int>());
        }
    }
    
    private int GetBoxId(int row, int col)
    {
        var boxSize = (int) Math.Sqrt(_length);  // Calculate the size of each box in a row/column
        var boxesPerRow = _length / boxSize;    // Number of boxes in each row/column
        
        // Calculate the box ID
        var boxRow = row / boxSize;
        var boxCol = col / boxSize;
        var boxId = boxRow * boxesPerRow + boxCol;
        
        return boxId;
    }
    
    private void Backtrack(int rowIndex, int columnIndex, char[][] board)
    {
        if (rowIndex == _length)
        {
            _solved = true;
            return;
        }

        var newRowIndex = columnIndex >= _length - 1 ? rowIndex + 1 : rowIndex;
        var newColumnIndex = columnIndex >=  _length - 1 ? 0 : columnIndex + 1;
        var boxIndex = GetBoxId(rowIndex, columnIndex);
        
        if (board[rowIndex][columnIndex] != EmptyCell)
        {
            Backtrack(newRowIndex, newColumnIndex, board);
            return;
        }
        
        var row = _rows[rowIndex];
        var column = _columns[columnIndex];
        var box = _boxes[boxIndex];

        for (var trie = 1; trie <= _length; trie++)
        {
            if (row.Contains(trie) || column.Contains(trie) || box.Contains(trie))
            {
                continue;
            }
            
            row.Add(trie);
            column.Add(trie);
            box.Add(trie);

            board[rowIndex][columnIndex] = (char)(trie + '0');
                
            Backtrack(newRowIndex, newColumnIndex, board);

            if (_solved)
            {
                return;
            }
                
            row.Remove(trie);
            column.Remove(trie);
            box.Remove(trie);
                
            board[rowIndex][columnIndex] = EmptyCell;
        }
    }
}