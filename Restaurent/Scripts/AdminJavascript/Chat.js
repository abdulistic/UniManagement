$(document).ready(function () {

    var chat = {
        messageToSend: '',
        messageResponses: [
            'Why did the web developer leave the restaurant? Because of the table layout.',
            'How do you comfort a JavaScript bug? You console it.',
            'An SQL query enters a bar, approaches two tables and asks: "May I join you?"',
            'What is the most used language in programming? Profanity.',
            'What is the object-oriented way to become wealthy? Inheritance.',
            'An SEO expert walks into a bar, bars, pub, tavern, public house, Irish pub, drinks, beer, alcohol'
        ],
        init: function () {
            this.cacheDOM();
            this.bindEvents();
            this.render();
        },
        cacheDOM: function () {
            this.$chatHistory = $('.chat-history');
            this.$button = $('button');
            this.$textarea = $('#message-to-send');
            this.$chatHistoryList = this.$chatHistory.find('ul');
        },
        bindEvents: function () {
            this.$button.on('click', this.addMessage.bind(this));
            this.$textarea.on('keyup', this.addMessageEnter.bind(this));
        },
        render: function () {
            this.scrollToBottom();
            if (this.messageToSend.trim() !== '') {
                var template = Handlebars.compile($("#message-template").html());
                var context = {
                    messageOutput: this.messageToSend,
                    time: this.getCurrentTime()
                };

                this.$chatHistoryList.append(template(context));
                this.scrollToBottom();
                this.$textarea.val('');

                // responses
                var templateResponse = Handlebars.compile($("#message-response-template").html());
                var contextResponse = {
                    response: this.getRandomItem(this.messageResponses),
                    time: this.getCurrentTime()
                };

                setTimeout(function () {
                    this.$chatHistoryList.append(templateResponse(contextResponse));
                    this.scrollToBottom();
                }.bind(this), 1500);

            }

        },

        addMessage: function () {
            this.messageToSend = this.$textarea.val()
            this.render();
        },
        addMessageEnter: function (event) {
            // enter was pressed
            if (event.keyCode === 13) {
                this.addMessage();
            }
        },
        scrollToBottom: function () {
            this.$chatHistory.scrollTop(this.$chatHistory[0].scrollHeight);
        },
        getCurrentTime: function () {
            return new Date().toLocaleTimeString().
                replace(/([\d]+:[\d]{2})(:[\d]{2})(.*)/, "$1$3");
        },
        getRandomItem: function (arr) {
            return arr[Math.floor(Math.random() * arr.length)];
        }

    };

    chat.init();

    var searchFilter = {
        options: { valueNames: ['name'] },
        init: function () {
            var userList = new List('people-list', this.options);
            var noItems = $('<li id="no-items-found">No items found</li>');

            userList.on('updated', function (list) {
                if (list.matchingItems.length === 0) {
                    $(list.list).append(noItems);
                } else {
                    noItems.detach();
                }
            });
        }
    };

    searchFilter.init();

});

var people;

$(document).ready(function () {
    getChatPeople(null);
});



function getSeachSuggest() {
    $.ajax(
        {
            url: "/student/GetChatUserList"
        }
    ).done(function (result) {
        $(".loaderblock").hide();
        $("#addedPeople").show();
        people = new Array();
        result.forEach(function (item) {
            item.UserName = item.FirstName + " " + item.LastName;
            people.push(item);
        });

    });
}


function getChat(userId) {
    $(".chatloaderblock").show();
    $("#chatHistory").hide();
    document.getElementById("chatHistory").innerHTML = "";

    $.ajax(
        {
            url: "/student/GetChat?id=" + userId
        }
    ).done(function (result) {
        $(".chatloaderblock").hide();
        $("#chatHistory").show();
        var currentUserId = $('#currentUserId').val();

        result.forEach(function (item) {
            var d = new Date(parseInt(item.CreatedOn.substr(6)));
            var time = d.getHours() + ":" + d.getMinutes() + ", " + d.toDateString();

            if (item.SenderId.toString() === currentUserId) {

                document.getElementById("chatHistory").innerHTML
                    += '<li>'
                    + '<div class="message-data">'
                    + '<span class="message-data-name"><i class="fa fa-circle online"></i> Me</span>'
                    + '<span class="message-data-time">' + time + '</span>'
                    + '</div>'
                    + '<div class="message my-message">'
                    + item.Message
                    + '</div>'
                    + '</li>'
            }
            else {

                document.getElementById("chatHistory").innerHTML
                    += '<li class="clearfix">'
                    + '<div class="message-data align-right">'
                    + '<span class="message-data-time">' + time + '</span> &nbsp; &nbsp;'
                    + '<span class="message-data-name">' + $("#selectedPersonName").text() + '</span> <i class="fa fa-circle me"></i>'
                    + '</div>'
                    + '<div class="message other-message float-right">'
                    + item.Message
                    + '</div>'
                    + '</li>'
            }

        });


    });

}

function getChatPeople(value) {
    $(".loaderblock").show();
    $("#addedPeople").hide();
    if (value) {

        $.ajax(
            {
                url: "/student/AddChatRoom?id=" + value
            }
        ).done(function (result) {
            if (result.length > 0) {
                document.getElementById("addedPeople").innerHTML = "";

                $("#recieverId").val(result[0].UserId);
                $("#chatRoomId").val(result[0].ChatRoomId);




                result.forEach(function (item) {
                    document.getElementById("addedPeople").innerHTML
                        += '<li class="clearfix">'
                        + '<a class="chatPerson" data-roomid="' + item.ChatRoomId + '" data-id="' + item.UserId + '" href="">'
                    + '<img height="40" src="/Images/people.png" alt="avatar" />'
                        + '<div class="about">'
                        + '<div style="color:aliceblue" class="name">' + item.FirstName + ' ' + item.LastName + '</div>'
                        + '<div class="status">'
                        + '</div>'
                        + '</div>'
                        + '</a>'
                        + '</li>'

                });

                $("#selectedPersonName").text(result[0].FirstName + ' ' + result[0].LastName);
                getChat(result[0].ChatRoomId)
            }


            getSeachSuggest();
        });
    }
    else {
        $.ajax(
            {
                url: "/student/GetChatPeople"
            }
        ).done(function (result) {

            if (result.length > 0) {
                document.getElementById("addedPeople").innerHTML = "";
                $("#recieverId").val(result[0].UserId);
                $("#chatRoomId").val(result[0].ChatRoomId);

                result.forEach(function (item) {

                    document.getElementById("addedPeople").innerHTML
                        += '<li class="clearfix">'
                        + '<a class="chatPerson" data-roomid="' + item.ChatRoomId + '" data-id="' + item.UserId + '" href="">'
                    + '<img height="40" src="/Images/people.png" alt="avatar" />'
                        + '<div class="about">'
                        + '<div style="color:aliceblue" class="name chatPerson">' + item.FirstName + ' ' + item.LastName + '</div>'
                        + '<div class="status">'
                        + '</div>'
                        + '</div>'
                        + '</a>'
                        + '</li>'

                });

                $("#selectedPersonName").text(result[0].FirstName + ' ' + result[0].LastName);
                getChat(result[0].ChatRoomId);
            }


            getSeachSuggest();
        });
    }




}

function matchPeople(input) {
    var reg = new RegExp(input.split("").join("\\w*").replace(/\W/, ""), "i");
    var res = [];
    if (input.trim().length === 0) {
        return res;
    }
    for (var i = 0, len = people.length; i < len; i++) {
        if (people[i].UserName.match(reg)) {
            res.push(people[i]);
        }
    }
    return res;
}

function changeInput(val) {
    var autoCompleteResult = matchPeople(val);
    document.getElementById("result").innerHTML = "";
    for (var i = 0, limit = 10, len = autoCompleteResult.length; i < len && i < limit; i++) {
        document.getElementById("result").innerHTML += "<a data-id='" + autoCompleteResult[i].UserId + "' class='userSelected list-group-item list-group-item-action' href='#' onclick='setSearch(\"" + autoCompleteResult[i].UserId + "\")'>" + autoCompleteResult[i].UserName + "</a>";
    }
}

function setSearch(value) {
    getChatPeople(value);
    document.getElementById("result").innerHTML = "";
}

$(document).on('click', '.chatPerson', function (e) {
    e.preventDefault();
    var usertId = $(this).attr('data-id');
    var roomid = $(this).attr('data-roomid');

    //var name = $(this).closest('.chatPerson').text();

    $("#selectedPersonName").text($(this).closest('.chatPerson').text());

    //var serialNumber = $tr.find('td[data-className]').data('className');

    getChat(roomid);

    $("#recieverId").val(usertId);
    $("#chatRoomId").val(roomid);
});

$(document).on('click', '#messageSendBtn', function (e) {
    e.preventDefault();

    var message = $('#message-to-send').val();
    if (message != '') {
        var recieverId = $('#recieverId').val();
        var chatRoomId = $('#chatRoomId').val();

        document.getElementById("chatHistory").innerHTML
            += '<li>'
            + '<div class="message-data">'
            + '<span class="message-data-name"><i class="fa fa-circle online"></i> Vincent</span>'
            + '<span class="message-data-time">10:12 AM, Today</span>'
            + '</div>'
            + '<div class="message my-message">'
            + message
            + '</div>'
            + '</li>'

        $('#message-to-send').val("")


        var dataObject = JSON.stringify({
            'ChatRoomId': chatRoomId,
            'Message': message,
            'RecieverId': recieverId,
        });

        $.ajax({
            url: '/student/addchat',
            type: 'POST',
            contentType: 'application/json',
            //done: submissionSucceeded,
            //fail: submissionFailed,
            data: dataObject
        }).done(function (result) {

        });
    }

});

        //$(document).on('keypress', function (e) {
        //    if (e.which == 13) {
        //        debugger

        //        $("#messageSendBtn").click();
        //    }
        //});
