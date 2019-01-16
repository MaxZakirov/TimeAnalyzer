import * as React from "react";
import { Pie, Doughnut, Radar, Polar, Bar } from 'react-chartjs-2';
import Profile from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService'
import DayChartService from './services/DayChartService';
import TimeRoller from './TimeRoller';
import TimeConverterService from "./services/TimeConverterService";
import TimeIntervalOption from "./TimeIntervalOption";
import MonthChartService from "./services/MonthChartService";
import ChangeValueForm from './ChangeValueForm';
import * as $ from 'jquery';

export default class Chart extends React.Component<any, any> {

    ReportsApi: TimeReportApiService;
    ChartService: any;
    TimeConverterService: TimeConverterService;
    chart: Chart = this;
    editWindowIsOpen: boolean = false;

    buttonToggle = false;

    constructor(props: any) {
        super(props);
        var initialChartData: any[] = [];
        this.state = {
            chartData: initialChartData,
            selectedDate: new Date(),
            monthCounter: new Date(),
            selectedTimeInterval: this.getTimeIntervalOptions()[0],
            showEditWindow: false,
            selectedReport: null
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
        this.onClickChart = this.onClickChart.bind(this);
        this.toggleForm = this.toggleForm.bind(this);
        this.openAddNewWindow = this.openAddNewWindow.bind(this);
        this.submitEditForm = this.submitEditForm.bind(this);
    }

    toggleForm() {

        $(".addReport").fadeToggle(0);
        $('.editWindow').toggle("slow");
        this.editWindowIsOpen = !this.editWindowIsOpen;
    }

    toggleChart() {
        $(".Doughnut").fadeToggle(0);
        $(".Bar").fadeToggle(0);
        if ($('.doughnutBtn').prop('disabled', true)) {
            $('.barId').prop('disabled', true);
            $('.doughnutBtn').prop('disabled', false);
        } else {
            $('.barId').prop('disabled', false);
            $('.doughnutBtn').prop('disabled', true);
        }
    }

    submitEditForm(date: any) {
        this.initializeDateChartData(date);
        this.closeEditWindow();
    }

    openAddNewWindow() {
        if (!this.editWindowIsOpen) {
            this.setState({
                selectedReport: -1
            });
        }

        this.toggleForm();
    }

    closeEditWindow() {
        if (this.editWindowIsOpen) {
            this.toggleForm();
        }
    }

    getTimeIntervalOptions() {
        return ['DAY', 'MONTH'];
    }

    initializeDateChartData(date: any): any {
        this.ReportsApi.getDayUserTimeReports(date)
            .then((res: any) => {
                this.setState({
                    selectedDate: date,
                    chartData: res.data.sort((a: any, b: any) => a.activity.typeId - b.activity.typeId)
                });
            });
    }

    initializeMonthChartData(monthCounter: any): any {
        this.ReportsApi.getUserTimeReportsInInterval(this.ChartService.getMonthStartDate(), this.ChartService.getMonthEndDate())
            .then((res: any) => {
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
        if (newOption === this.state.selectedTimeInterval)
            return;

        this.setState({
            selectedTimeInterval: newOption
        });

        switch (newOption) {
            case 'DAY':
                this.ChartService = new DayChartService();
                this.initializeDateChartData(this.state.selectedDate);
                break;
            case 'MONTH':
                this.closeEditWindow();
                this.ChartService = new MonthChartService(this.state.monthCounter);
                this.initializeMonthChartData(this.state.monthCounter);
                return;
        }
    }

    getChartData(): any {
        var reports = this.ChartService.fillEmpty(this.state.chartData);

        var chartValues = reports.map((dataObject: any) => dataObject.duration);
        var chartLabels = reports.map((dataObject: any) => dataObject.activity.name);
        var chartColors = reports.map((dataObject: any) => dataObject.activity.type.colorValue);

        return {
            labels: chartLabels,
            datasets: [{
                label: 'TimeAnalizer',
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
        var onClick = this.onClickChart;
        return {
            legend: {
                display: true,
                position: 'left',
                labels: {
                    fontSize: 15,
                    fontColor: '#eee'
                }
            },
            onClick: onClick
        };
    }

    getChartOptionsBar(): any {
        var onClick = this.onClickChart;
        return {
            legend: {
                fullWidth: true,
                display: false,
                position: 'top',
                labels: {
                    boxWidth: 40,
                    fontSize: 12,
                    fontColor: '#eee'
                }
            },
            onClick: onClick
        };
    }

    onClickChart(e: Event, data: any) {
        if (data[0]) {
            var selectedReport = this.state.chartData[data[0]._index];
            if (selectedReport.id > 0) {
                this.setState({
                    selectedReport: selectedReport
                });
                if (!this.editWindowIsOpen) {
                    this.toggleForm();
                }
            }
        }
    }

    getAddNewReportBtn() {
        if (this.state.selectedTimeInterval === 'DAY') {
            return <button className="btn btn-primary addReport" onClick={this.openAddNewWindow}>
                Add new report
                    </button>
        }
    }

    render() {
        return (
            <div className="mainPage">
                <div className="container">
                    <div className="row">
                        <div className="col-sm-4">
                            {this.getRoller()}
                        </div>
                        <div className="col-sm-4">
                            <div className="toggle">
                                <button className="doughnutBarBtn" onClick={this.toggleChart}>
                                    ToggleChart
                            </button>
                            </div>
                        </div>
                        <div className="col-sm-4">
                            {this.getTimeIntervalOptions().map((option: any) =>
                                <TimeIntervalOption
                                    isActive={option == this.state.selectedTimeInterval}
                                    option={option}
                                    changeOption={this.onTimeIntervalChange}
                                />
                            )}
                        </div>
                    </div>
                    <div className="Doughnut">
                        <Doughnut
                            data={this.getChartData()}
                            options={this.getChartOptions()}
                        />
                    </div>
                    <div className="Bar">
                        <Bar
                            data={this.getChartData()}
                            options={this.getChartOptionsBar()}
                        />
                    </div>
                    <div>
                        <div className="col-sm-4 editWindow">
                            <ChangeValueForm
                                selectedReport={this.state.selectedReport}
                                onSubmit={this.submitEditForm}
                                selectedDate={this.state.selectedDate}
                            />
                            <button className="closeAddReport" onClick={this.toggleForm}>
                                <i className="glyphicon glyphicon-remove"></i>
                            </button>
                        </div>
                        <div>
                            {this.getAddNewReportBtn()}
                        </div>
                    </div>
                </div>
            </div>
        )
    }
}