import React from 'react';
import '../styles/contact.css';
const Contact = () => {
    return (
        <div className="contact-background">
            <div className="jumbotron contact-bg">
                <div className="contact-text">
                    <div className="contact-header">Contact</div>
                    <div className="contact-header-text">Contact us</div>
                </div>
            </div>
            <div className="container">
                <div className="row d-flex mb-5 contact-info">
                    <div className="w-100"></div>
                    <div className="col-md-3 d-flex">
                        <div className="info bg-white p-4">
                            <p><span>Address:</span> <a>Đà Lạt</a> </p>
                        </div>
                    </div>
                    <div className="col-md-3 d-flex">
                        <div className="info bg-white p-4">
                            <p><span>Phone:</span> <a>123456789</a></p>
                        </div>
                    </div>
                    <div className="col-md-3 d-flex">
                        <div className="info bg-white p-4">
                            <p><span>Email:</span> <a>info@yoursite.com</a></p>
                        </div>
                    </div>
                    <div className="col-md-3 d-flex">
                        <div className="info bg-white p-4">
                            <p><span>Website</span> <a href="#">yoursite.com</a></p>
                        </div>
                    </div>
                </div>
                <div >
                    <div className="col-md-6 order-md-last d-flex">
                        <form action="#" >
                            <div className="form-group">
                                <input type="text" className="form-control" placeholder="Your Name" />
                            </div>
                            <div className="form-group">
                                <input type="text" className="form-control" placeholder="Your Email" />
                            </div>
                            <div className="form-group">
                                <textarea name="" id="" cols="15" rows="4" className="form-control" placeholder="Message"></textarea>
                            </div>
                            <div className="form-group">
                                <input type="submit" value="Send Message" className="btn btn-primary py-3 px-5" />
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    )
}


export default Contact