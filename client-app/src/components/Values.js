import React from 'react';
import value1 from '../assets/images/value-1.png';
import value2 from '../assets/images/value2.png';
import value3 from '../assets/images/value3.jpg';

function Values() {
    return (
        <div className="container values">
            <div className="row text-center">
                <div className="col-md-4">
                    <img className="img-circle img-responsive img-center values-img" src={value1} alt="Farmer Value" />
                    <h2>Benefits for Farmers</h2>
                    <p>Farmers will get <b>30% More Revenue</b> and <b>One Stop Sale</b> with <b>Garuntteed payments</b> in 24 hours.</p>
                </div>
                <div className="col-md-4">
                    <img className="img-circle img-responsive img-center values-img" src={value2} alt="Retailer Value" />
                    <h2>Convenient for Retailers</h2>
                    <p>Retailers are Convenient with <b>Competitive pricing</b> and <b>Door Delivery</b> and also it Saves their <b>Time</b> </p>
                </div>
                <div className="col-md-4">
                    <img className="img-circle img-responsive img-center values-img" src={value3} alt="Consumers Value" />
                    <h2>Savings for Consumers</h2>
                    <p>Consumers are happy with <b>Pricing and Hygenic </b> and <b>Delivered Directly from Farm</b></p>
                </div>
            </div>
        </div>
    )
}

export default Values
