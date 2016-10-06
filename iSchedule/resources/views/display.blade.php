@extends('layout.master')
@section('title', 'Successful Excel File Upload')


@push('javascript')
<script>
$(function(){
    $('#users-table').DataTable({
       select:true,
        columnDefs: [{ //temporary solution to not show pop-up
        "defaultContent": "-",
        "targets": "_all" }]
    });
});
</script>
@endpush



@section('content')
    <table class="table table-bordered" id="users-table">
        <thead>
            <tr>
                <th>Number</th>
                <th>FirstName</th>
                <th>LastName</th>
            </tr>
        </thead>
        <tbody>
        @foreach($list as $item)
        <tr>
            <td>{{$item["Number"]}}</td>
            <td>{{$item["FirstName"]}}</td>
            <td>{{$item["LastName"]}}</td>
        <tr>
        @endforeach
        </tbody>
    </table>
@endsection



<!-- @section('content')
    <table class="table table-bordered" id="users-table">
        <thead>
            <tr>
                <th>Number</th>
                <th>FirstName</th>
                <th>LastName</th>
            </tr>
        </thead>
        <tbody>
        @foreach($list as $item)
        <tr>
            <td>{{$item->number}}</td>
            <td>{{$item->firstName}}</td>
            <td>{{$item->lastName}}</td>
        <tr>
        @endforeach
        </tbody>
    </table>
@endsection -->

