jQuery.fn.shake = function (intShakes, intDistance, intDuration) {
    this.each(function () {
        $(this).css("position", "relative");
        for (var x = 1; x <= intShakes; x++) {
            $(this).animate({ left: (intDistance * -1) }, (((intDuration / intShakes) / 4)))
                .animate({ left: intDistance }, ((intDuration / intShakes) / 2))
                .animate({ left: 0 }, (((intDuration / intShakes) / 4)));
        }
    });
    return this;
};

function UpdateGrid(gridName) {
    gridName.UpdateEdit();
}
function CancelGrid(gridName) {
    gridName.CancelEdit();
}
function showError(error) {
    $.ajax({
        url: "/transaction-error",
        data: { error: error },
        success: function (e) {
            $('#error-container').html(e);
            hideLoading();
            $("#error-container").effect("shake", { distance: 10, times: 1 });
        }
    });

}
var sticker = 0;
function computeStickerUnit() {
    var payloaderNew = parseInt(PayloaderNew.GetValue() || 0);
    var payloaderOld = parseInt(PayloaderOld.GetValue()) || 0;
    var backhoeNew = parseInt(BackhoeNew.GetValue() || 0);
    var backhoeOld = parseInt(BackhoeOld.GetValue() || 0);
    var haulingTrucksFourteenNew = parseInt(HaulingTrucksFourteenNew.GetValue() || 0);
    var haulingTrucksFourteenOld = parseInt(HaulingTrucksFourteenOld.GetValue() || 0);
    var haulingTrucksTenNew = parseInt(HaulingTrucksTenNew.GetValue() || 0);
    var haulingTrucksTenOld = parseInt(HaulingTrucksTenOld.GetValue() || 0);
    var haulingTrucksSixNew = parseInt(HaulingTrucksSixNew.GetValue() || 0);
    var haulingTrucksSixnOld = parseInt(HaulingTrucksSixnOld.GetValue() || 0);
    var plantsNew = parseInt(PlantsNew.GetValue() || 0);
    var plantsOld = parseInt(PlantsOld.GetValue() || 0);
    var potteriesandCementNew = parseInt(PotteriesandCementNew.GetValue() || 0);
    var potteriesandCementOld = parseInt(PotteriesandCementOld.GetValue() || 0);

    sticker = payloaderNew +
        payloaderOld +
        backhoeNew +
        backhoeOld +
        haulingTrucksFourteenNew +
        haulingTrucksFourteenOld +
        haulingTrucksTenNew +
        haulingTrucksTenOld +
        haulingTrucksTenOld +
        haulingTrucksSixNew +
        haulingTrucksSixnOld +
        plantsNew +
        plantsOld +
        potteriesandCementNew +
        potteriesandCementOld;
    StickerUnit.SetValue(sticker);
}


