import React from 'react'
import '../styles/carosel.css'
import slider2 from '../assets/images/slider2-min.png'
import slider12 from '../assets/images/slider12-min.png'

function Carosel(props) {
    return (
        <div className="container-fluid" >
            <div id="myCarousel" className="carousel slide carousel-bg" data-ride="carousel">
                <ol className="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" className="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                </ol>

                <div className="carousel-inner">
                    <div className="item active">
                        <img src={slider2} className="pull-right" alt="Los Angeles" />
                        <div className="carousel-caption">
                            <h3>Fruits</h3>
                            <p>Fruits delivered directly from farmers!</p>
                            <button type="submit" className="btn btn-cust" onClick={() => props.buy("products")} >Buy With us</button>
                        </div>
                    </div>

                    <div className="item">
                        <img src={slider12} className="pull-right" alt="Chicago" />
                        <div className="carousel-caption">
                            <h3>Vegetables</h3>
                            <p>Fresh from the farm and Hygenic!</p>
                            <button type="submit" className="btn btn-cust" onClick={() => props.buy("products")} >Buy With us</button>
                        </div>
                    </div>
                </div>

                <a className="left carousel-control" href="#myCarousel" data-slide="prev">
                    <span className="glyphicon glyphicon-chevron-left"></span>
                    <span className="sr-only">Previous</span>
                </a>
                <a className="right carousel-control" href="#myCarousel" data-slide="next">
                    <span className="glyphicon glyphicon-chevron-right"></span>
                    <span className="sr-only">Next</span>
                </a>
            </div>
        </div>
    )
}

export default Carosel
