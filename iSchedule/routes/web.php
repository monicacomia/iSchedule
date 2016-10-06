<?php

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| This file is where you may define all of the routes that are handled
| by your application. Just tell Laravel the URIs it should respond
| to using a Closure or controller method. Build something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});


Route::get('/schedule', function () {
    return view('schedule');
});


Route::get('/upload','UploadFileController@index');
Route::post('/upload','UploadFileController@showUploadFile');

// Route::post('/downloaderrorfile','DownloadErrorFileController@downloadFile');
 Route::get('/errorfile',function(){
 	return view('errorfile');
 });


Route::get('/display',function(){
 	return view('display');
 });

// Route::controller('datatables', 'UploadFileController', [
//      'getArray'  => 'datatables.data',
//      'showDisplay' => 'datatables',y
//  ]);

Route::get('/datatables','UploadFileController@getArray');


Route::get('/download', 'DownloadExcelFileController@createExcel');

 //  Route::controller('datatables', 'UploadFileController', [
 //      'getArray'  => 'datatables.data',
 // 	  'showDisplay' => 'datatables'
 // ]);