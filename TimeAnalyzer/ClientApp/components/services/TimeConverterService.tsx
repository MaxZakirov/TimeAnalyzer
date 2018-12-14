import * as React from 'react';

export default class TimeConverterService extends React.Component<any, any> {
    
    constructor(){
        super();
     }

     toServerFormatDate(date: any) {
        var day = +date.getDate() >= 10 ? date.getDate() : '0' + date.getDate();
        var month = +date.getMonth() + 1  >= 10 ? date.getMonth() + 1 : '0' + date.getMonth();
        var year = +date.getFullYear() >= 10 ? date.getFullYear() : '0' + date.getFullYear();
        var hour = +date.getHours() >= 10 ? date.getHours() : '0' + date.getHours();
        var minute = +date.getMinutes() >= 10 ? date.getMinutes() : '0' + date.getMinutes();
        var second = +date.getSeconds() >= 10 ? date.getSeconds() : '0' + date.getSeconds();
        
        return day + "&" + month + "&" + year + "_" + hour + ':' + minute + ':' + second; 
     }

     getMonthName(monthId: any) {
        const monthNames = ["January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
      ];
      
      return monthNames[monthId];
     }
}