import * as React from "react";
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import Profile from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService'
import DayChartService from './services/DayChartService';
import TimeRoller from './TimeRoller';
import TimeConverterService from "./services/TimeConverterService";
import TimeIntervalOption from "./TimeIntervalOption";
import MonthChartService from "./services/MonthChartService";

export default class Chart extends React.Component<any, any> {

    ReportsApi: TimeReportApiService;
    ChartService: any;
    TimeConverterService: TimeConverterService;
    chart: Chart = this;

    constructor(props: any) {
        super(props);
        var initialChartData: any[] = [];
        this.state = {
            chartData: initialChartData,
            selectedDate: new Date(),
            monthCounter: new Date(),
            selectedTimeInterval: this.getTimeIntervalOptions()[0]
        }

        this.ChartService = new DayChartService();
        this.ReportsApi = new TimeReportApiService();
        this.TimeConverterService = new TimeConverterService();

        this.rollBackDate = this.rollBackDate.bind(this);
        this.rollForwardDate = this.rollForwardDate.bind(this);
        this.initializeDateChartData = this.initializeDateChartData.bind(this);
        this.onTimeIntervalChange = this.onTimeIntervalChange.bind(this);
        this.rollForwardMonth = this.rollForwardMonth.bind(this);
        this.rollBackMonth = this.rollBackMonth.bind(this);
        this.getMonthPresentationView = this.getMonthPresentationView.bind(this);
    }

    getTimeIntervalOptions() {
        return ['DAY', 'MONTH'];
    }

    initializeDateChartData(date: any): any {
        this.ReportsApi.getDayUserTimeReports(date)
            .then((res: any) => {
                this.setState({
                    selectedDate: date,
                    chartData: res.data
                });
            });
    }

    initializeMonthChartData(monthCounter: any): any {
        this.ReportsApi.getUserTimeReportsInInterval(this.ChartService.getMonthStartDate(), this.ChartService.getMonthEndDate())
            .then((res: any) => {
                debugger;
                this.setState({
                    monthCounter: monthCounter,
                    chartData: res.data.reports
                });
            });
    }

    componentDidMount() {
        this.initializeDateChartData(new Date());
    }

    rollBackDate() {
        var date = new Date(this.state.selectedDate);
        date.setDate(date.getDate() - 1);
        this.initializeDateChartData(date);
    }

    getDatePresentationView() {
        return this.state.selectedDate.getDate() + ' ' + this.TimeConverterService.getMonthName(this.state.selectedDate.getMonth());
    }

    rollForwardDate() {
        var date = new Date(this.state.selectedDate);
        date.setDate(date.getDate() + 1);
        this.initializeDateChartData(date);
    }

    rollBackMonth() {
        debugger;
        var date = new Date(this.state.monthCounter);
        date.setMonth(date.getMonth() - 1);
        this.ChartService.setMonth(date);
        this.initializeMonthChartData(date);
    }

    rollForwardMonth() {
        var date = new Date(this.state.monthCounter);
        date.setMonth(date.getMonth() + 1);
        this.ChartService.setMonth(date);
        this.initializeMonthChartData(date);
    }

    getMonthPresentationView() {
        return this.TimeConverterService.getMonthName(this.state.monthCounter.getMonth());
    }

    onTimeIntervalChange(newOption: any) {
        this.setState({
            selectedTimeInterval: newOption
        });

        switch (newOption) {
            case 'DAY':
                this.ChartService = new DayChartService();
                this.initializeDateChartData(this.state.selectedDate);
                break;
            case 'MONTH':
                this.ChartService = new MonthChartService(this.state.monthCounter);
                this.initializeMonthChartData(this.state.monthCounter);
                return;
        }
    }

    getChartData(): any {
        var reports = this.ChartService.fillEmpty(this.state.chartData);

        var chartValues = reports.map((dataObject: any) => dataObject.duration);
        var chartLabels = reports.map((dataObject: any) => dataObject.activity.name);
        var chartColors = reports.map((dataObject: any) => dataObject.activity.colorValue);

        return {
            labels: chartLabels,
            datasets: [{
                label: 'Power',
                data: chartValues,
                backgroundColor: chartColors,
                borderWidth: 2,
                borderColor: '#fff',
                hoverBorderWidth: 2,
                hoverBorderColor: '#eee'
            }]
        };
    }

    getRoller() {
        switch (this.state.selectedTimeInterval) {
            case 'DAY':
                return <TimeRoller
                    dateString={this.getDatePresentationView()}
                    rollBack={this.rollBackDate}
                    rollForward={this.rollForwardDate}
                />
            case 'MONTH':
                return <TimeRoller
                    dateString={this.getMonthPresentationView()}
                    rollBack={this.rollBackMonth}
                    rollForward={this.rollForwardMonth}
                />;
        }

    }

    getChartOptions(): any {
        return {
            legend: {
                display: true,
                position: 'left',
                labels: {
                    fontSize: 15,
                    fontColor: '#eee'
                }
            }
        };
    }

    render() {
        return (
            <div className="mainPage">
                <div className="container">
                    <div className="row">
                        <div className="col-sm-3">
                            {this.getRoller()}
                        </div>
                        <div>
                            {this.getTimeIntervalOptions().map((option: any) =>
                                <TimeIntervalOption
                                    isActive={option == this.state.selectedTimeInterval}
                                    option={option}
                                    changeOption={this.onTimeIntervalChange}
                                />
                            )}
                        </div>
                    </div>
                    <Doughnut
                        data={this.getChartData()}
                        options={this.getChartOptions()}
                    />
                </div>
            </div>
        )
    }
}