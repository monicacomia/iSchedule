<?php

namespace App\Http\Controllers;


use Illuminate\Support\Facades\Input; //Input
use Illuminate\Http\Request;
use Maatwebsite\Excel\Facades\Excel;
use App\Http\Requests;
use Illuminate\Support\Facades\App;
use DB;
use View;
use Datatables;


class UploadFileController extends Controller
{ 

  private $errorArray=array();
  private $fileHasError=null;
     public function index()
     {
      return view('upload');
   	 }

     //currently not in use
     public function showDisplay()
    {

        return redirect('/display');
    }

    //currently not in use
    public function getArray(){
          $collection=$this->errorArray;
         return $collection;
    }

   public function showUploadFile(){

   	if(Input::hasfile('excel')){
   		$file= Input::file('excel');
   		$excel = App::make('excel');
       $excel->load($file, function ($reader) { // or Excel::load($file,function($reader))
       		//$reader->dump();
              
              //$count=1; //doesn't work when pass to function below
              
              $reader->each(function($sheet,$count) {
              
              $count++;
              
              $index=$count-1;

              

              //echo $count;
              //echo '<br>';

              $containsError=false;
              $error=" Contains error: ";


              if(empty($sheet["Number"])){
                $error .=" Missing number ";
                $containsError=true;
              }

              if(empty($sheet["LastName"])){
                $error .=" Missing LastName ";
                $containsError=true;
              }


              if(empty($sheet["FirstName"])){
                $error .=" Missing FirstName ";
                $containsError=true;
              }




             if($containsError){
                
                $sheet["Remarks"]=$error;
                $this->fileHasError=true;
               // echo $error;
                //echo '<br>';




             }

             $this->errorArray[$index]=$sheet;

            


             // DB::table('trial')->insert(
             //  array('number' => $sheet["Number"],
             //        'lastName' => $sheet["LastName"],
             //        'firstName' => $sheet["FirstName"]
             //        ));
             
             
             
              //insert into database..i dont know why $sheet is working instead of $rows
              // DB::table('trial')->insert(
              // array('number' => $sheet["Number"],
              //       'lastName' => $sheet["LastName"],
              //       'firstName' => $sheet["FirstName"]
              //       ));


            // This displays each cell contents instead of by rows    
    				// $sheet->each(function($row) {
    				// 	echo $row;
    				// 	echo '<br>';

    				// });

				  
       });


      // if($this->fileHasError){
      // //if there is an error return to view with an array that contains excel with error remarks
      // //the view will then have a button that will create an error excel file if pushed     
      // //return View::make('errorfile',['errorArray'=>$this->errorArray,"hasError"=>$this->fileHasError]);
      //   return view('errorfile'); //not going to errorfile
      // }

      // else{
      //   echo "File has no error";
      // }
      // foreach ($this->errorArray as $value) {
      //   echo $value;
      // }




   	});
   		

   }

     if($this->fileHasError){
      //if there is an error return to view with an array that contains excel with error remarks
      //the view will then have a button that will create an error excel file if pushed     
      

      //return View::make('errorfile',['errorArray'=>$this->errorArray,"hasError"=>$this->fileHasError]);
      $errorList=$this->errorArray;
      return view('errorfile',compact('errorList'));  
      //not going to errorfile
      }

      else{

      //$list=DB::table('trial')->get();
      $list=$this->errorArray;
      return view('display',compact('list'));

      }
}

}
