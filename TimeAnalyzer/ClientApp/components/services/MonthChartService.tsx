import * as React from 'react';
import DayChartService from './DayChartService'

export default class MonthChartService extends React.Component<any, any> {

    DayChartService: DayChartService;
    monthCounter: any;

    constructor(monthCounter: any) {
        super();
        this.DayChartService = new DayChartService();
        this.monthCounter = monthCounter;
    }

    fillEmpty(timeReports: any) {
        var endDate = this.getMonthEndDate();
        var startDate = this.getMonthStartDate();

        var newReports = timeReports.slice();
        var sumOfDuration = this.DayChartService.getDurationSumOfReports(newReports);
        var minutesInMonth = ((endDate.getDate() - startDate.getDate() + 1) * this.DayChartService.minutesInDay);
        var minutesRemain = minutesInMonth - sumOfDuration;

        if (minutesRemain > 0) {
            var undefinedTimeReport = this.DayChartService.getUndefinedTimeReport();
            undefinedTimeReport.duration = minutesRemain;
            newReports.push(undefinedTimeReport);
        }

        return newReports;
    }

    getMonthStartDate() {
        return new Date(this.monthCounter.getFullYear(), this.monthCounter.getMonth(), 1);
    }

    getMonthEndDate() {
        return new Date(this.monthCounter.getFullYear(), this.monthCounter.getMonth() + 1, 0);
    }

    setMonth(counter: any) {
        this.monthCounter = counter;
    }
}