$(document).ready(function () {
    



    $('#drop').click(function () {
        $('#nav').slideToggle(); 
         
        
    });


    $(function () {
        $(".first-menu").hover(function () {
            $(this).find("#first-menu").stop().slideDown(0);
           
        }, function() {
            $(this).find("#first-menu").stop().slideUp(0);
        });
        
        $(".socend-menu").hover(function () {
            $(".socend-menu").find("#socend-menu").stop().slideToggle(0);
            return false;
        });
     
        $(".socend-menu:last").mouseleave(function () {
         $("#menu ul li ul").stop().slideUp(0);
        return false;
       });
      
        
    });
    
    //$(function () {
    //    $(".socend-menu").hover(function () {
    //        $(this).find("ul").stop().slideToggle(0);

    //        if ($(this).find("ul").children().length<=0) {
    //            $(this).find("ul").remove();
    //        }
           
            
        
     
    //        return false;
    //    });

        $(".socend-menu").hover(function () {
            $(this).find("#socend-menu").stop().slideToggle(0);
            return false;
        });
        
     

    //});
    

    $(function () {
        
  
    });

    $(function () {
        $("#more-menu h3").click(function () {
            $("#more-menu-slide").stop().slideToggle();
            return false;
        });

        $(".more-menu").hover(function () {
            $(this).find("#sub-more-menu").stop().slideToggle(0);
            return false;
        });

    });





 
        $(".content").hide();  
        $(".title:first").next().show(); 
                       
        $(".title").click(function(){  
                       
            if ( $(this).next().is(':hidden')  ) { 
                                   
                $(".title").next().slideUp("200");  
                                   
                $(this).next().slideDown("200"); 
 
            }
            return false;                                     });                            



});

