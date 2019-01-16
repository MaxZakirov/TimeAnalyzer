import * as React from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService';
import ActivitiesService from './services/ActivitiesService';
import TimeConverterService from './services/TimeConverterService';


export default class ChangeValueForm extends React.Component<any, any>{
    timeReport: TimeReportApiService;
    ActivitiesService: ActivitiesService;
    TimeConverterService: TimeConverterService;

    currentTypedValue: any;
    currentTypedDate: any;


    constructor(props: any) {
        super(props);
        this.timeReport = new TimeReportApiService();

        this.state = {
            reportId: null,
            activities: [],
            date: new Date(),
            minutes: 60,
            selectedActivityId: -1,
            selectedActivityTypeId: -1,
            error: null
        };

        this.ActivitiesService = new ActivitiesService();
        this.TimeConverterService = new TimeConverterService();

        this.updateRepotrActivity = this.updateRepotrActivity.bind(this);
        this.updateReportDate = this.updateReportDate.bind(this);
        this.updateReportMinutes = this.updateReportMinutes.bind(this);
        this.updateRepotrActivityType = this.updateRepotrActivityType.bind(this);
        this.onSumbit = this.onSumbit.bind(this);
        this.onError = this.onError.bind(this);
        this.getDefaultState = this.getDefaultState.bind(this);
    }

    initializeActivities() {
        this.ActivitiesService.getAllActivities()
            .then((res: any) => {
                
                if (this.state.selectedActivityId == -1) {
                    var activityTypeId = res.data[0].type.id;
                    var activityId = res.data[0].id;
                }
                else {
                    var activityTypeId = this.state.selectedActivityTypeId;
                    var activityId = this.state.selectedActivityId;
                }

                this.setState({
                    selectedActivityTypeId: activityTypeId,
                    selectedActivityId: activityId,
                    activities: res.data
                });
            })
    }

    componentDidMount() {
        this.initializeActivities();
    }

    componentWillReceiveProps(props: any) {
        
        if(props.selectedReport === -1)
        {
            this.setState(this.getDefaultState(props.selectedDate));
        }
        if (props.selectedReport != null) {
            var date = this.TimeConverterService.fromServerDate(props.selectedReport.date);
            this.setState({
                reportId: props.selectedReport.id,
                date: date,
                minutes: props.selectedReport.duration,
                selectedActivityId: props.selectedReport.activityId,
                selectedActivityTypeId: props.selectedReport.activity.typeId,
            });
        }
    }

    getDefaultState(date: any) {
        var activities = this.state.activities;
        return {
            reportId: null,
            date: date,
            minutes: 60,
            selectedActivityId: activities[0].id,
            selectedActivityTypeId: activities[0].type.id
        }
    }

    updateRepotrActivity(event: any) {
        this.setState({
            selectedActivityId: event.target.value
        });
    }

    updateRepotrActivityType(event: any) {
        
        var newSelectedActivityTypeId = +event.target.value; 
        var newActivityId = this.state.activities.filter((a: any) => a.typeId === newSelectedActivityTypeId)[0].id;
        this.setState({
            selectedActivityTypeId: newSelectedActivityTypeId,
            selectedActivityId: newActivityId
        });
    }

    updateReportDate(e: any) {
        var date = new Date(e.currentTarget.value);
        this.setState({
            date: date
        })
    }

    updateReportMinutes(e: any) {
        this.setState({
            minutes: +e.currentTarget.value
        });
    }

    onSumbit(e: any) {
        e.preventDefault();
        var date = new Date(this.state.date);
        var duration = this.state.minutes;
        var activityId = this.state.selectedActivityId;
        if (this.state.reportId > 0) {
            var reportId = this.state.reportId;
            this.timeReport.updateTimeReport(reportId, date, duration, activityId).then((response: any) => {
                this.setState({
                    error: null
                });
                this.props.onSubmit(date);
            }, this.onError);
        }
        else {
            this.timeReport.addTimeReport(date, duration, activityId).then((response: any) => {
                this.props.onSubmit(date);
            }, this.onError);
        }
    }

    getHtmlFormatDate(date: any) {
        return date.getFullYear() + '-' + '0' + (date.getMonth() + 1) + '-' + date.getDate();
    }

    getHeader() {
        if (this.state.reportId > 0) {
            return "Update report";
        }
        else {
            return "Add new report";
        }
    }

    onError(err: any) {
        this.setState({
            error: err.response.data
        });
    }

    getError() {
        if (this.state.error != null) {
            return <p className="alert alert-danger">
                {this.state.error}
            </p>
        }
    }

    render() {
        return (
            <div className="windowNuts">
                <h2 className="text-center">{this.getHeader()}</h2>
                <form onSubmit={this.onSumbit}>
                    <div>
                        <div>
                            <div className="form-group">
                                <label htmlFor="ReportDate">Date</label>
                                <input className="form-control" name="reportDate"
                                    type="date" value={this.getHtmlFormatDate(this.state.date)} onChange={this.updateReportDate} />
                            </div>
                            <div className="form-group">
                                <label htmlFor="reportMinutes">Minutes</label>
                                <input className="form-control" name="reportMinutes"
                                    min={1}
                                    max={1440}
                                    type="number" value={this.state.minutes}
                                    onChange={this.updateReportMinutes}
                                    placeholder="type your value"></input>
                            </div>
                            <div className="form-group">
                                <label htmlFor="reportMinutes">Actvity type</label>
                                <select className="form-control" value={this.state.selectedActivityTypeId} onChange={this.updateRepotrActivityType}>
                                    {this.state.activities.map((a: any) => a.type).filter((value: any, index: any, self: any) => self.map((t: any) => t.id).indexOf(value.id) === index).map((activityType: any) => {
                                        return <option value={activityType.id} key={activityType.id}>
                                            {activityType.name}
                                        </option>
                                    })}
                                </select>
                            </div>
                            <div className="form-group">
                                <label htmlFor="reportMinutes">Actvity</label>
                                <select className="form-control" value={this.state.selectedActivityId} onChange={this.updateRepotrActivity}>
                                    {this.state.activities.filter((a: any) => a.typeId === this.state.selectedActivityTypeId).map((a: any) =>
                                        <option value={a.id} key={a.id}>
                                            {a.name}
                                        </option>
                                    )}
                                </select>
                            </div>
                        </div>
                    </div>
                    {this.getError()}
                    <button className="form-control btn btn-success" type="submit">Add</button>
                </form>
            </div>
        )
    }
}