@extends('layout.master')
@section('title', 'Upload Excel File')



@section('content')


	<div class="secure">Upload form</div>
	<div>
	{!! Form::open(array('url'=>'/upload','method'=>'POST', 'files'=>true)) !!}
	{!! Form::file('excel') !!}
	{!! Form::submit('Upload File') !!}
    {!! Form::close() !!}     

    {!! Form::open(array('url'=>'/download','method'=>'GET')) !!}
	{!! Form::submit('Download Excel File') !!}
    {!! Form::close() !!}    
    </div>
	
	
	
 @endsection