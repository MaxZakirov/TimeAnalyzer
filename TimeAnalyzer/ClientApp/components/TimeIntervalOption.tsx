import * as React from "react";

const TimeIntervalOption = (props: any) => {
    var style = 'text-right option';
    
    if(props.isActive)
    {
        style += ' active'
    }

    return (
        <h3 className={style}
        onClick={() => props.changeOption(props.option)}>
            <b>
                {props.option}
            </b>
        </h3>
    );
};

export default TimeIntervalOption;