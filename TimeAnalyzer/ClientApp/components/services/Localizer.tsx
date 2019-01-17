import * as React from 'react';

interface Window { [key:string]: any  }

class Localizer extends React.Component<any, any> {

    constructor() {
        super();
    }

    getString(name: any)
    {
        debugger;
        if(typeof (window) !== "undefined")
        {
            var resources = (window as any)["resources"];
            return resources.filter((r: any) => r.name === name)[0].value;
        }
        else 
        {
            return "ErrorLocalization";
        }
    }
}

export default Localizer;