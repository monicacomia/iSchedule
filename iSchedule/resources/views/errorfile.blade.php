@extends('layout.master')
@section('title', 'Error Excel File')

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
                <th>Remarks</th>
            </tr>
        </thead>
        <tbody>
        @foreach($errorList as $item)
        <tr>
            <td>{{$item["Number"]}}</td>
            <td>{{$item["FirstName"]}}</td>
            <td>{{$item["LastName"]}}</td>
            <td>{{$item["Remarks"]}}</td>
        <tr>
        @endforeach
        </tbody>
    </table>
@endsection

