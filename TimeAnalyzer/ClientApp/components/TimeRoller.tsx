import * as React from "react";

const TimeRoller = (props: any) => {
    return (
        <div className="mainDateBlock">
            <button className="btn-block dayRollerBtn" onClick={props.rollBack}><i className="glyphicon glyphicon-triangle-top"></i></button>
            <h3 className="text-center date">{props.dateString}</h3>
            <button className="btn-block dayRollerBtn" onClick={props.rollForward}><i className="glyphicon glyphicon-triangle-bottom"></i></button>
        </div>
    );
};

export default TimeRoller;