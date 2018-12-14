import * as React from "react";
import { Pie, Doughnut, Radar, Polar } from 'react-chartjs-2';
import Profile from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService'
import ChartService from './services/ChartService';
import DayRoller from './dayRoller';
import TimeConverterService from "./services/TimeConverterService";

export default class Chart extends React.Component<any, any> {

    ReportsApi: TimeReportApiService;
    ChartService: ChartService;
    TimeConverterService: TimeConverterService;
    chart: Chart = this;

    constructor(props: any) {
        super(props);
        var initialChartData: any[] = [];
        this.state = {
            chartData: initialChartData,
            selectedDate: new Date()
        }

        this.ChartService = new ChartService();
        this.ReportsApi = new TimeReportApiService();
        this.TimeConverterService = new TimeConverterService();

        this.rollBackDate = this.rollBackDate.bind(this);
        this.rollForwardDate = this.rollForwardDate.bind(this);
        this.initializeChartData = this.initializeChartData.bind(this);
    }

    initializeChartData(date: any): any {
        debugger;
        this.ReportsApi.getDayUserTimeReports(date)
            .then((res: any) => {
                this.setState({
                    selectedDate: date,
                    chartData: res.data
                });
            });
    }

    componentDidMount() {
        this.initializeChartData(new Date());
    }

    rollBackDate() {
        var date = new Date(this.state.selectedDate);
        date.setDate(date.getDate() - 1);
        this.initializeChartData(date);
    }

    getDatePresentationView() {
        return this.state.selectedDate.getDate() + ' ' + this.TimeConverterService.getMonthName(this.state.selectedDate.getMonth());
    }

    rollForwardDate() {
        var date = new Date(this.state.selectedDate);
        date.setDate(date.getDate() + 1);
        this.initializeChartData(date);
    }

    getChartData(): any {
        var reports = this.ChartService.fillEmptyPartOfDay(this.state.chartData);
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
                <DayRoller 
                    dateString={this.getDatePresentationView()}
                    rollBack={this.rollBackDate}
                    rollForward={this.rollForwardDate}
                    />
                <Doughnut
                        data={this.getChartData()}
                        options={this.getChartOptions()}
                    />
                </div>
            </div>
        )
    }
}