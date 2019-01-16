import * as React from 'react';
import Chart from './Chart';
import RadioButton from './ChartRadioButton';
import TimeReportApiService from './services/TimeReportApiService';
import ActivitiesService from './services/ActivitiesService';
import TimeConverterService from './services/TimeConverterService';
import SuggestionsService from "./services/SuggestionsService";


export default class Suggestions extends React.Component<any, any>{

    suggestionsService: SuggestionsService;

    currentTypedValue: any;
    currentTypedDate: any;


    constructor(props: any) {
        super(props);

        this.state = {
            date: new Date(),
            allTypes: [],
            selectedType: 'Loading',
            suggestedActivities: [],
            allSuggestions: []
        };

        this.suggestionsService = new SuggestionsService();
    }

    componentDidMount() {
        this.suggestionsService.getAllSuggestions(this.state.date)
            .then((res: any) => {
                debugger;
                var allTypes = res.data.map((kv: any) => kv.activityType);
                var selectedType = allTypes[0];
                var suggestedActivities = res.data.filter((kv: any) => kv.activityType.id === selectedType.id)[0].activities;
                var allSuggestions = res.data;
                this.setState({
                    allTypes: allTypes,
                    selectedType: selectedType,
                    suggestedActivities: suggestedActivities,
                    allSuggestions: allSuggestions
                });
            });
    }

    render() {
        return (
            <div className="suggestionsBlock">
                <h1 className="text-center">Suggestions</h1>

                <h2>{this.state.selectedType.name}</h2>



                <ol>
                    {this.state.suggestedActivities.map((a: any) => {
                        return (<h3 key={a.id}><li>{a.name}</li></h3>);
                    })}
                </ol>
            </div>
        );
    }
}