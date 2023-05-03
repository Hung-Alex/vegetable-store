import React from 'react';
import '../styles/filter.css';
import { data } from '../data.js';

const Filter = (props) => {
    const menuItems = [...new Set(data.map((Val) => Val.courseName))];
    return (
        <div className="row sticky searchFilter" style={{ width: "1214px" }}>
            <div className="col-md-4">
                <div className="inner-addon left-addon">
                    <i className="glyphicon glyphicon-search"></i>
                    <input className="form-control" type="text" value={props.search} onChange={(e) => props.handleSearch(e.target.value)} placeholder="Search products..." />
                </div>
            </div>
            <div className="col-md-4">
                <select className="form-control select" onChange={(e) => props.handleChangeSort(e.target.value)} >
                    <option value="">Order By</option>
                    <option value="lowest">Lowest to Highest</option>
                    <option value="highest">Highest to Lowest</option>
                </select>
            </div>
        </div>
    )
}

export default Filter;