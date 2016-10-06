@extends('layout.master')
@section('title', 'Download Excel File')



@section('content')


	<div class="secure">Upload form</div>
	<div>
	{!! Form::open(array('url'=>'/download','method'=>'GET')) !!}
	{!! Form::submit('Download Excel File') !!}
    {!! Form::close() !!}     
    </div>
	
	
	
 @endsection