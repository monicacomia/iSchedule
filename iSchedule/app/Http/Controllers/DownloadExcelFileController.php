<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

use App\Http\Requests;

use Excel;

class DownloadExcelFileController extends Controller
{
    public function createExcel(){

    	Excel::create('template',function($excel){
			
			$header[]=['Number','FirstName','LastName','Remarks'];

    		$excel->setTitle('Schedule');
    		$excel->sheet('sheet1', function($sheet) use ($header) {
            $sheet->fromArray($header, null, 'A1', false, false);
            //$sheet->getStyle("D4:D100")->getProtection()->setLocked(PHPExcel_Style_Protection::PROTECTION_PROTECTED); Shit doesnt work
            //$sheet->protectCells('D2'); doesn't work
        })->download('xlsx');


    	});
    }
}
