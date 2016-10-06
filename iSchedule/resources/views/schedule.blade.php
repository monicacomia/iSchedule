@extends('layout.master')
@section('title', 'Schedule')


@push('javascript')
    <script src="{{ asset('js/dhtmlxscheduler.js') }}" type="text/javascript"></script>
   <link rel="stylesheet" href="{{ asset('css/dhtmlxscheduler.css') }}" type="text/css">

  
   <style type="text/css" media="screen">
    html, body{
        margin:0px;
        padding:0px;
        height:100%;
        overflow:hidden;
    }   
    </style>
@endpush



@section('content')
<div id="scheduler_here" class="dhx_cal_container" style='width:100%; height:100%;'>
    <div class="dhx_cal_navline">
        <div class="dhx_cal_prev_button">&nbsp;</div>
        <div class="dhx_cal_next_button">&nbsp;</div>
        <div class="dhx_cal_today_button"></div>
        <div class="dhx_cal_date"></div>
        <div class="dhx_cal_tab" name="day_tab" style="right:204px;"></div>
        <div class="dhx_cal_tab" name="week_tab" style="right:140px;"></div>
        <div class="dhx_cal_tab" name="month_tab" style="right:76px;"></div>
    </div>
    <div class="dhx_cal_header"></div>
    <div class="dhx_cal_data"></div>       
</div>

<script type="text/javascript">
       
         document.body.onload = function() {
            

            //var utc = new Date().toJSON().slice(0,10);

            var currentDate = new Date();
            var day = currentDate.getDate();
            var month = currentDate.getMonth();
            var year = currentDate.getFullYear();
            scheduler.config.xml_date="%Y-%m-%d %H:%i";
            scheduler.init('scheduler_here',new Date(year,month,day),"day"); //loads the current day

            /*  Example of json binding to the scheduler
                    var events = [
                        {id:1, text:"Meeting", start_date:"10/01/2016 08:00",end_date:"10/01/2016 11:00"},
                        {id:2, text:"Conference",start_date:"10/01/2016 08:00",end_date:"10/01/2016 11:00"}
              ];
                  scheduler.parse(events,"json"); 

            */

            //Database binding
            scheduler.load("{{ asset('connector/connector.php') }}");
            var dp = new dataProcessor("{{ asset('connector/connector.php') }}");
            dp.init(scheduler);
           };
           


</script>
@endsection

