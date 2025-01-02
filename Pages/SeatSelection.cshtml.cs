using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Airline.Models;

namespace Airline.Pages
{
    public class SeatSelection : PageModel
    {
        private readonly SeatSelectionModel _seatSelectionModel;

        [BindProperty]
        public SeatSelectionContainerObject SeatSelectionVM { get; set; }

        public SeatSelection()
        {
            _seatSelectionModel = new SeatSelectionModel();
            SeatSelectionVM = new SeatSelectionContainerObject();
        }

        public void OnGet()
        {
            SeatSelectionVM.SearchPerformed = false;
        }

        public IActionResult OnPost()
        {
            try
            {
                // Validate boarding pass
                if (!_seatSelectionModel.ValidateBoardingPass(SeatSelectionVM.BoardingID, SeatSelectionVM.FlightID))
                {
                    SeatSelectionVM.ErrorMessage = "Invalid boarding pass or flight ID.";
                    SeatSelectionVM.SearchPerformed = true;
                    return Page();
                }

                // Get available seats
                SeatSelectionVM.AvailableSeats = _seatSelectionModel.GetAvailableSeats(SeatSelectionVM.FlightID);
                
                if (SeatSelectionVM.AvailableSeats == null || SeatSelectionVM.AvailableSeats.Count == 0)
                {
                    SeatSelectionVM.ErrorMessage = "No seats available for this flight.";
                }
            }
            catch (Exception ex)
            {
                SeatSelectionVM.ErrorMessage = "An error occurred while retrieving seat information.";
                // Log the exception if needed
            }

            SeatSelectionVM.SearchPerformed = true;
            return Page();
        }

        public IActionResult OnPostSelectSeat()
        {
            try
            {
                if (string.IsNullOrEmpty(SeatSelectionVM.SelectedSeatID))
                {
                    SeatSelectionVM.ErrorMessage = "Please select a seat.";
                    return OnPost();
                }

                bool success = _seatSelectionModel.UpdateSeatSelection(
                    SeatSelectionVM.BoardingID,
                    SeatSelectionVM.SelectedSeatID
                );

                if (!success)
                {
                    SeatSelectionVM.ErrorMessage = "Failed to update seat selection.";
                    return OnPost();
                }

                return RedirectToPage("./SeatSelection", new { success = true });
            }
            catch (Exception ex)
            {
                SeatSelectionVM.ErrorMessage = "An error occurred while updating seat selection.";
                return OnPost();
            }
        }
    }
}
