﻿using _10_1_TicTacToe.Models;
using Microsoft.AspNetCore.Mvc;

namespace _10_1_TicTacToe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ViewResult Index() {
            var board = new TicTacToeBoard();

            var cells = board.GetCells();
            foreach (Cell cell in cells)
            {
                cell.Mark = TempData[cell.Id]?.ToString();
            }
            board.CheckForWinner();

            // create view moodel to pass to view
            var model = new TicTacToeViewModel {
                Cells = cells,
                Selected = new Cell { Mark = TempData["nextTurn"]?.ToString() ?? "X" }, // add default for first time
                IsGameOver = board.HasWinner || board.HasAllCellsSelected
            };

            if (model.IsGameOver) {
                TempData["nextTurn"] = "X"; // reset mark
                TempData["message"] = (board.HasWinner) ? $"{board.WinningMark} wins!" : "It's a tie";
            }
            else
            { // game continues -  keep data in TempData
                TempData.Keep();
            }

            return View(model);
        }

        [HttpPost]
        public RedirectToActionResult Index(TicTacToeViewModel vm)
        {
            // store saelected cell in TempData
            TempData[vm.Selected.Id] = vm.Selected.Mark;

            // determine next turn based on current mark and store in TempData
            TempData["nextTurn"] = (vm.Selected.Mark == "X") ? "O" : "X";

            return RedirectToAction("Index");
        }
    }
}
