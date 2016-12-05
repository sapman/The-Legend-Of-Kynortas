var creatorApp = angular.module("app", ['ngDragDrop']);

creatorApp.controller('ctrl', ['$scope', function ($scope) {
    $scope.recalc = function () {

        WIDTH = [];
        for (var i = 0; i < $scope.width; i++) {
            WIDTH.push(i);
        }
        $scope.Windex = WIDTH;


        HEIGHT = [];
        for (var i = 0; i < $scope.height; i++) {
            HEIGHT.push(i);
        }
        $scope.Hindex = HEIGHT;

    }
    $scope.now = new Date();
    $scope.mapName = ""
    $scope.width = 1;
    $scope.height = 1;
    $scope.recalc();
    $scope.output = "// Created by " + $scope.cretName + "public static TileMap Create" + $scope.mapName + $scope.width;


}]);