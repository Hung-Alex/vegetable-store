import React, { useState } from 'react';
import '../styles/contact.css';
import { useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { AddToContact } from '../services/ContactRepository';
const Contact = () => {
    const user = useSelector((state) => state.auth.login.currentUser);
    const token = user?.result.token;
    const navigate = useNavigate();

    const initialState = {
        id: 0,
        title: '',
        description: '',
        email: '',
        meta: '',
        shippingDate: '',
        status: 0
    },
        [feedback, setFeedback] = useState(initialState);

    const hanldeSubmit = (e) => {
        e.preventDefault();
        if (user === null) {
            navigate("/signin");
        }
        let form = new FormData();
        form.append("id", String(feedback.id));
        form.append("title", String(feedback.title));
        form.append("description", String(feedback.description));
        form.append("email", String(feedback.email));
        form.append("meta", String(feedback.meta));
        form.append("status", String(feedback.status));

        AddToContact(form, token, navigate).then(data => {
            if (data === true) {
                alert("Error!");
            } else {
                alert("Send contact success!");
                navigate("/");
            }
        })
    }

    return (
        <div className="container text-center" style={{ marginTop: "100px" }}>
            <div className="row">
                <div className="col-sm-5 col-md-6">
                    <div class="container contact-form">
                        <div class="contact-image">
                            {/* <img src="https://image.ibb.co/kUagtU/rocket_contact.png" alt="rocket_contact" /> */}
                        </div>
                        <form onSubmit={hanldeSubmit}>
                            <div class="row">
                                <div class="col-md-6">
                                    <h3>Drop Us a Message</h3>
                                    <div class="form-group">
                                        <input type="text" name="txtTitle" class="form-control" placeholder="Your Title *" required
                                            onChange={(e) => setFeedback({
                                                ...feedback,
                                                title: e.target.value
                                            })} />
                                    </div>
                                    <div class="form-group">
                                        <input type="email" name="txtEmail" class="form-control" placeholder="Your Email *" required
                                            onChange={(e) => setFeedback({
                                                ...feedback,
                                                email: e.target.value
                                            })} />
                                    </div>
                                    <div class="form-group">
                                        <textarea name="txtMsg" class="form-control" placeholder="Your Description *" required
                                            onChange={(e) => setFeedback({
                                                ...feedback,
                                                description: e.target.value
                                            })}></textarea>
                                    </div>
                                    <div class="form-group">
                                        <textarea name="txtMsg" class="form-control" placeholder="Your Meta *" style={{ width: "100%", height: "150px" }} required
                                            onChange={(e) => setFeedback({
                                                ...feedback,
                                                meta: e.target.value
                                            })}></textarea>
                                    </div>
                                    <div class="form-group">
                                        <input type="submit" name="btnSubmit" class="btnContact" value="Send Message" />
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
                <div className="col-sm-5 offset-sm-2 col-md-6 offset-md-0">
                    <div className="mapouter">
                        <div className="gmap_canvas">
                            <iframe className="gmap_iframe" frameBorder={0} scrolling="no" marginHeight={0} marginWidth={0} src="https://maps.google.com/maps?width=600&height=400&hl=en&q=DaLat&t=&z=14&ie=UTF8&iwloc=B&output=embed" />
                            <a href="https://capcuttemplate.org/">Capcut Templates</a>
                        </div>
                        <style dangerouslySetInnerHTML={{ __html: ".mapouter{position:relative;text-align:right;width:600px;height:400px;}.gmap_canvas {overflow:hidden;background:none!important;width:600px;height:400px;}.gmap_iframe {width:600px!important;height:400px!important;}" }} />
                    </div>
                </div>
            </div>
        </div>
    )
}

export default Contact;