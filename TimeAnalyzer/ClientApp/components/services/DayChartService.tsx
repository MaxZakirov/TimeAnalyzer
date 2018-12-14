import * as React from 'react';

class DayChartService extends React.Component<any, any> {

    minutesInDay: any = 1440;
    month: any;

    constructor() {
        super();
    }

    fillEmpty(timeReports: any) {
        var newReports = timeReports.slice();
        var sumOfDuration = this.getDurationSumOfReports(newReports);
        var minutesRemain = this.minutesInDay - sumOfDuration;
        if (minutesRemain > 0) {
            var undefinedTimeReport = this.getUndefinedTimeReport();
            undefinedTimeReport.duration = minutesRemain;
            newReports.push(undefinedTimeReport);
        }
        
        return newReports;
    }

    getUndefinedTimeReport() {
        return {
            id: 0,
            duration: 0,
            activityId: 0,
            activity: {
                id: 0,
                name: 'Unknown',
                iconPath: null,
                colorValue: '#C6C6C6'
            }
        }
    }

    getDurationSumOfReports(reports: any) {
        return reports.map((r: any) => r.duration)
        .reduce((a: any, b: any) => a + b, 0);
    }
}

export default DayChartService;