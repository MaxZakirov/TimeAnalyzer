import * as React from "react";

const RadioButton = (props: any) => {

    return (
        <p>
            <input
                className="check"
                type="radio"
                name="check"
                checked={props.checked}
                onChange={() => props.handleChange(props.id)}>
            </input>
            <label>{props.labelName}</label>
        </p>
    );
};

export default RadioButton;