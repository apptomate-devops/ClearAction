// JavaScript Document

$(document).ready(function () {
	"use strict";
    $('input').click(function () {
		
		
        $('input:not(:checked)').parent().parent().removeClass("active");
        $('input:checked').parent().parent().addClass("active");
    });    
});